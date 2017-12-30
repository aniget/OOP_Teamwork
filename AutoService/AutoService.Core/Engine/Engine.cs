using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoService.Core.Factory;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core
{

    public sealed class Engine : IEngine
    {
        //TODO CHECK WHOLE DOCUMENT FOR REPETITIVE CODE

        private readonly IList<IEmployee> employees;
        private readonly IList<BankAccount> bankAccounts;
        private readonly ICollection<ICounterparty> clients;
        private readonly ICollection<ICounterparty> suppliers;
        private readonly IDictionary<IClient, List<ISell>> notInvoicedSells;

        private DateTime lastInvoiceDate =
            DateTime.ParseExact("2017-01-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        private DateTime lastAssetDate = DateTime.ParseExact("2017-01-30", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private int lastInvoiceNumber = 0;
        private IAutoServiceFactory factory;

        private static readonly IEngine SingleInstance = new Engine();

        //constructor
        private Engine()
        {
            this.factory = new AutoServiceFactory();
            this.employees = new List<IEmployee>();
            this.bankAccounts = new List<BankAccount>();
            this.clients = new List<ICounterparty>();
            this.suppliers = new List<ICounterparty>();
            this.notInvoicedSells = new Dictionary<IClient, List<ISell>>();
        }

        public static IEngine Instance
        {
            get { return SingleInstance; }
        }

        public void Run()
        {
            var command = ReadCommand();
            var commandParameters = new string[] { string.Empty };

            while (command != "exit")
            {
                commandParameters = ParseCommand(command);
                try
                {
                    ExecuteSingleCommand(commandParameters);
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(Environment.NewLine + "<>-<>-<>-<>-<>-<>-<>-<>---<>-<>-<>-<>-<>-<>-<>-<>" + Environment.NewLine);
                command = ReadCommand();
            }
        }

        private string ReadCommand()
        {
            return Console.ReadLine();
        }

        private string[] ParseCommand(string command)
        {
            return command.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ExecuteSingleCommand(string[] commandParameters)
        {
            string commandType = "";

            try
            {
                commandType = commandParameters[0];
            }
            catch
            {
                //this catch the IndexOutOfRange Exception and throws a message "Unknown command" which is the default of the Switch
            }
            int employeeId;
            decimal ratePerMinute;
            IEmployee employee;
            ICounterparty supplier;
            //IOrderStock orderStock;
            IStock stock;
            string position;
            DepartmentType department;
            VehicleType vehicleType;
            EngineType engineType;
            string assetName;
            string supplierName;
            string supplierAddress;
            string supplierUniquieNumber;

            switch (commandType)
            {
                #region showEmployees

                case "showEmployees":

                    if (this.employees.Count == 0)
                    {
                        throw new ArgumentException("There are no employees! Hire them!");
                    }
                    this.ShowEmployees();
                    break;


                #endregion

                case "hireEmployee":

                    ValidateExactParameterLength(commandParameters, 7);

                    var firstName = commandParameters[1];
                    var lastName = commandParameters[2];
                    position = commandParameters[3];
                    decimal salary = decimal.TryParse(commandParameters[4], out salary)
                        ? salary
                        : throw new ArgumentException("Please provide a valid decimal value for salary!");
                    ratePerMinute = decimal.TryParse(commandParameters[5], out ratePerMinute)
                        ? ratePerMinute
                        : throw new ArgumentException("Please provide a valid decimal value for rate per minute!");
                    ;

                    if (!Enum.TryParse(commandParameters[6], out department))
                    {
                        string[] ListOfDepartments = Enum.GetNames(typeof(DepartmentType));
                        StringBuilder builder = new StringBuilder();
                        foreach (var dept in ListOfDepartments)
                        {
                            builder.AppendLine(dept);
                        }
                        throw new ArgumentException("Department is not valid!" + Environment.NewLine + "List of departments to choose from:" + Environment.NewLine + builder);
                    }

                    this.AddEmployee(firstName, lastName, position, salary, ratePerMinute, department);
                    break;

                case "fireEmployee":

                    ValidateExactParameterLength(commandParameters, 2);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");
                    this.FireEmployee(employee);
                    break;

                case "changeEmployeeRate":

                    ValidateExactParameterLength(commandParameters, 3);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    ratePerMinute = decimal.TryParse(commandParameters[2], out ratePerMinute)
                        ? ratePerMinute
                        : throw new ArgumentException("Please provide a valid decimal value for rate per minute!");

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    this.ChangeRateOfEmployee(employee, ratePerMinute);
                    break;

                case "issueInvoices":
                    ValidateExactParameterLength(commandParameters, 1);

                    this.IssueInvoices();
                    break;

                case "showAllEmployeesAtDepartment":

                    ValidateExactParameterLength(commandParameters, 2);

                    if (!Enum.TryParse(commandParameters[1], out department))
                    {
                        throw new ArgumentException("Department not valid!");
                    }

                    this.ListEmployeesAtDepartment(department);
                    break;

                case "changeEmployeePosition":
                    ValidateExactParameterLength(commandParameters, 3);


                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    position = commandParameters[2];
                    this.ChangePositionOfEmployee(employee, position);
                    break;

                case "addEmployeeResponsibility":

                    ValidateMinimumParameterLength(commandParameters, 3);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    var responsibilitiesToAdd = commandParameters.Skip(2).ToArray();
                    this.AddResponsibilitiesToEmployee(employee, responsibilitiesToAdd);

                    break;

                case "removeEmpoloyeeResponsibility":

                    ValidateMinimumParameterLength(commandParameters, 3);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();
                    this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

                    break;
                case "addClientsCar":
                    if (commandParameters.Length < 8)
                    {
                        throw new NotSupportedException(
                            "Add car to client  command must be at least 8 parameters!");
                    }
                    var uniaqueNumbre = commandParameters[1];
                    var vehicleMake = commandParameters[2];
                    var vehicleModel = commandParameters[3];
                    if (!Enum.TryParse(commandParameters[4], out vehicleType))
                    {
                        throw new ArgumentException("Vehicle Type not valid!");
                    }
                    var registrationNumber = commandParameters[5];
                    var vehicleYear = commandParameters[6];
                    if (!Enum.TryParse(commandParameters[7], out engineType))
                    {
                        throw new ArgumentException("EngineType Type not valid!");
                    }
                    var additionalParams = commandParameters[8];
                
                    var currClient = (Client)this.clients.FirstOrDefault(x => x.UniqueNumber == uniaqueNumbre);
                    
                    if (currClient == null)
                    {
                        throw new ArgumentException($"The are no client with this {uniaqueNumbre}.");
                    }
                    if (currClient.Vehicles.Any(x => x.RegistrationNumber == registrationNumber))
                    {
                        throw new ArgumentException($"This client already has a vehicle with this registration number: {registrationNumber}.");
                    }
                    var curcar = CreateVehicle(vehicleType, vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, additionalParams);
                    currClient.Vehicles.Add((Vehicle)curcar);
                    break;
                case "createBankAccount":

                    ValidateExactParameterLength(commandParameters, 3);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "No employees currently in the service! You need to hire one then open the bank account :)");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    assetName = commandParameters[2];

                    DateTime currentAssetDate = this.lastAssetDate.AddDays(5);

                    this.CreateBankAccount(employee, assetName, currentAssetDate);
                    break;

                case "depositCashInBank":

                    ValidateExactParameterLength(commandParameters, 3);

                    if (this.bankAccounts.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "No bank accounts currently opened! You need to open one then deposit the cash.");
                    }

                    int bankAccountId = int.TryParse(commandParameters[1], out bankAccountId)
                        ? bankAccountId
                        : throw new ArgumentException("Please provide a valid integer value for bankAccount Id!");

                    if (bankAccountId <= 0)
                    {
                        throw new ArgumentException(
                            $"Please provide a valid bankAccount Id, i.e. between 1 and {this.bankAccounts.Count}!");
                    }

                    var bankAccount = this.bankAccounts.Count >= bankAccountId
                        ? this.bankAccounts[bankAccountId - 1]
                        : throw new ArgumentException("This bank account does not exist!");

                    decimal depositAmount = decimal.TryParse(commandParameters[2], out depositAmount)
                        ? depositAmount
                        : throw new ArgumentException("Please provide a valid decimal value for depositAmount!");

                    this.DepositCashInBankAccount(bankAccount, depositAmount);
                    break;

                case "orderStockToWarehouse":

                    ValidateMinimumParameterLength(commandParameters, 4);

                    var emplFN = commandParameters[1];
                    var supplN = commandParameters[2];
                    var stockName = commandParameters[3];
                    var purchasePrice = decimal.Parse(commandParameters[4]);

                    if (this.employees.Select(x => x.FirstName == emplFN).Count() > 1)
                    {
                        throw new ArgumentException("More than one emplyee with same name, please provide both first name");
                        //TODO: case with FN and Id
                    }
                    else if (!this.employees.Any(x => x.FirstName == emplFN))
                    {
                        throw new ArgumentException($"There is no employee {emplFN} in the AutoService");
                    }

                    employee = this.employees.Single(x => x.FirstName == emplFN);

                    if (this.suppliers.Select(x => x.Name == supplN).Count() > 1)
                    {
                        throw new ArgumentException("More than one registered supplier with same name, please provide unique number INSTEAD of name");
                    }
                    else if (!this.suppliers.Any(x => x.Name == supplN))
                    {
                        throw new ArgumentException($"Our AutoService does not work with supplier {supplN}");
                    }

                    supplier = this.suppliers.Single(x => x.Name == supplN);

                    stock = new Stock(stockName, employee, purchasePrice, supplier);

                    //OrderStock orderStock = null;
                    ////this.OrderStockToday(orderedStockClass, emplFN, supplN);

                    this.OrderStockFromSupplier(stock);
                    break;

                case "registerSupplier":

                    //ValidateMinimumParameterLength(commandParameters, 2); //validation is made in the class

                    supplierName = commandParameters[1];
                    supplierAddress = commandParameters[2];
                    supplierUniquieNumber = commandParameters[3];

                    this.AddSupplier(supplierName, supplierAddress, supplierUniquieNumber);
                    break;

                case "removeSupplier":

                    ValidateMinimumParameterLength(commandParameters, 2);

                    supplierName = commandParameters[1];

                    if (commandParameters.Length == 3)
                    {
                        supplierUniquieNumber = commandParameters[2];
                        this.RemoveSupplier(supplierName, supplierUniquieNumber);
                    }
                    else
                    {
                        this.RemoveSupplier(supplierName);
                    }
                    break;

                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
        }

        private void DepositCashInBankAccount(BankAccount bankAccount, decimal depositAmount)
        {
            bankAccount.DepositFunds(depositAmount);
            Console.WriteLine($"{depositAmount} $ were successfully added to bank account {bankAccount.Name}");
        }

        private void CreateBankAccount(IEmployee employee, string assetName, DateTime currentAssetDate)
        {
            if (employee.Responsibiities.Contains(ResponsibilityType.Account) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage))
            {
                BankAccount bankAccountToAdd = this.factory.CreateBankAccount(assetName, employee, currentAssetDate);

                this.bankAccounts.Add(bankAccountToAdd);
                Console.WriteLine(
                    $"Asset {assetName} was created successfully by his responsible employee {employee.FirstName} {employee.LastName}");
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {employee.FirstName} {employee.LastName} does not have the required repsonsibilities to register asset {assetName}");
            }

        }

        private IVehicle CreateVehicle(VehicleType vehicleType, string vehicleModel, string vehicleMake, string registrationNumber, string vehicleYear, EngineType engineType, string additionalParams)
        {
            IVehicle vehicle = null;

            if (vehicleType == VehicleType.Car)
            {
                vehicle = (IVehicle)this.factory.CreateCar(vehicleModel, vehicleMake, int.Parse(additionalParams), registrationNumber, vehicleYear, engineType);
            }
            else if (vehicleType == VehicleType.SmallTruck)
            {
                vehicle = (IVehicle)this.factory.CreateSmallTruck(vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, int.Parse(additionalParams));
            }
            else if (vehicleType == VehicleType.Truck)
            {
                vehicle = (IVehicle)this.factory.CreateTruck(vehicleModel, vehicleMake, registrationNumber, vehicleYear,engineType,int.Parse(additionalParams));
            }
            return vehicle;
        }

        private void RemoveResponsibilitiesToEmployee(IEmployee employee, string[] responsibilitiesToRemove)
        {
            var responsibilitesToAdd = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilitiesToRemove)
            {
                ResponsibilityType currentResponsibility;
                if (!Enum.TryParse(responsibility, out currentResponsibility))
                {
                    throw new ArgumentException($"Responsibility {responsibility} not valid!");

                }
                responsibilitesToAdd.Add(currentResponsibility);
            }
            employee.RemoveResponsibilities(responsibilitesToAdd);
        }

        private void AddResponsibilitiesToEmployee(IEmployee employee, string[] responsibilities)
        {
            var responsibilitesToAdd = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilities)
            {
                ResponsibilityType currentResponsibility;
                if (!Enum.TryParse(responsibility, out currentResponsibility))
                {
                    throw new ArgumentException($"Responsibility {responsibility} not valid!");
                }
                responsibilitesToAdd.Add(currentResponsibility);
            }
            employee.AddResponsibilities(responsibilitesToAdd);
        }

        private void ChangePositionOfEmployee(IEmployee employee, string position)
        {
            employee.ChangePosition(position);

            Console.WriteLine(
                $"Position of employee {employee.FirstName} {employee.LastName} was successfully set to {position}");
        }

        private void ListEmployeesAtDepartment(DepartmentType department)
        {
            if (this.employees.Any(x => x.Department != department))
            {
                throw new ArgumentException($"The are no employees at department: {department}!");
            }

            StringBuilder str = new StringBuilder();

            str.AppendLine($"Employees at: {department} department:");
            foreach (var employee in employees)
            {
                var counter = 1;
                if (employee.Department == department)
                {

                    str.AppendLine($"{counter}. {employee.ToString()}");
                    counter++;
                    //TODO ADD DISTINGUISHER BETWEEN LINES (#########?)
                }
            }
            Console.WriteLine(str.ToString());
        }

        private void ChangeRateOfEmployee(IEmployee employee, decimal ratePerMinute)
        {
            employee.ChangeRate(ratePerMinute);
            Console.WriteLine(
                $"Rate per minute of employee {employee.FirstName} {employee.LastName} was successfully set to {ratePerMinute} $");
        }

        
        //private void OrderStockFromSupplier(/*OrderStock o, */IEmployee employee, ICounterparty supplier, string stockName, decimal purchasePrice)
        private void OrderStockFromSupplier(IStock stock)
        {
            if (stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse) ||
                stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.Manage))
            { 
                IOrderStock orderStock = factory.CreateOrderStock(stock.ResponsibleEmployee, stock.PurchasePrice, TypeOfWork.Ordering, stock.Supplier, stock);
                orderStock.OrderStockToWarehouse(stock.ResponsibleEmployee.FirstName, stock.Supplier.Name, stock.Name, stock.PurchasePrice);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required repsonsibilities to register asset {stock.Name}");
            }
            //orderStock.OrderStockToWarehouse(employee.FirstName, supplier.Name, stockName, purchasePrice);

            Console.WriteLine($"{stock.Name} ordered from {stock.Supplier.Name} for the amount of {stock.PurchasePrice} are stored in the Warehouse." + Environment.NewLine + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        private void ShowEmployees()
        {
            int hiredCounter = 1;
            if (this.employees.Where(e => e.IsHired).Count() > 0)
            {
                Console.WriteLine("Current active employees:");
                foreach (var currentEmployee in this.employees.Where(e => e.IsHired))
                {

                    Console.WriteLine(hiredCounter + ". " + currentEmployee);
                    hiredCounter++;
                }
                int counter = 1;
                foreach (var currentEmployee in this.employees)
                {
                    Console.WriteLine(counter + ". " + currentEmployee.ToString());
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("No active employees!");
            }

            int firedCounter = 1;

            if (this.employees.Where(e => !e.IsHired).Count() > 0)
            {
                Console.WriteLine("Current fired employees:");
                foreach (var currentEmployee in this.employees.Where(e => !e.IsHired))
                {

                    Console.WriteLine(firedCounter + ". " + currentEmployee.ToString());
                    firedCounter++;
                }
            }
            else
            {
                Console.WriteLine("No fired employees!");
            }
        }

        private void IssueInvoices()
        {
            int invoiceCount = 0;
            foreach (var client in this.notInvoicedSells.OrderBy(o => o.Key.Name))
            {
                this.lastInvoiceNumber++;
                invoiceCount++;
                string invoiceNumber = this.lastInvoiceNumber.ToString().PadLeft(10, '0');
                this.lastInvoiceDate.AddDays(1);
                IInvoice invoice = new Invoice(invoiceNumber, this.lastInvoiceDate, client.Key);

                foreach (var sell in client.Value)
                {
                    invoice.InvoiceItems.Add(sell);
                }
                var clientToAddInvoice =
                    this.clients.FirstOrDefault(f => f.UniqueNumber == client.Key.UniqueNumber);
                clientToAddInvoice.Invoices.Add(invoice);
            }

            this.notInvoicedSells.Clear();
            Console.WriteLine($"{invoiceCount} invoices were successfully issued!");
        }

        private void FireEmployee(IEmployee employee)
        {
            employee.FireEmployee();

            Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }

        private void AddEmployee(string firstName, string lastName, string position, decimal salary,
            decimal ratePerMinute, DepartmentType department)
        {
            IEmployee employee =
                this.factory.CreateEmployee(firstName, lastName, position, salary, ratePerMinute, department);

            this.employees.Add(employee);
            Console.WriteLine(employee);
            Console.WriteLine($"Employee {firstName} {lastName} added successfully with Id {this.employees.Count}");
        }

        private void AddSupplier(string name, string address, string uniqueNumber)
        {
            ICounterparty supplier = this.factory.CreateSupplier(name, address, uniqueNumber);
            if (suppliers.FirstOrDefault(x => x.UniqueNumber == uniqueNumber) != null)
            {
                throw new ArgumentException("Supplier with the same unique number already exist. Please check the number and try again!");
            }
            this.suppliers.Add(supplier);
            Console.WriteLine(supplier);
            Console.WriteLine($"Supplier {name} added successfully with Id {this.suppliers.Count}!");
        }

        private void RemoveSupplier(string name)
        {
            ICounterparty supplierToBeRemoved = this.suppliers.FirstOrDefault(x => x.Name == name);
            if (supplierToBeRemoved == null)
            {
                throw new ArgumentException($"Supplier with name {name} cannot be removed - no such supplier found");
            }
            else if (this.suppliers.Select(x => x.Name == name).Count() > 1)
            {
                throw new ArgumentException($"More than one supplier with name {name} exist." + Environment.NewLine + "Please provide both name and uniquire number of the supplier you want to remove");
            }
            this.suppliers.Remove(supplierToBeRemoved);
            Console.WriteLine(supplierToBeRemoved);
            Console.WriteLine($"Supplier {name} removed successfully!");
        }

        private void RemoveSupplier(string name, string uniqueNumber)
        {
            ICounterparty supplierToBeRemoved = this.suppliers.FirstOrDefault(x => x.Name == name && x.UniqueNumber == uniqueNumber);
            if (supplierToBeRemoved == null)
            {
                throw new ArgumentException($"Supplier with name {name} and uniquie number {uniqueNumber} cannot be removed - no such supplier found");
            }
            this.suppliers.Remove(supplierToBeRemoved);
            Console.WriteLine(supplierToBeRemoved);
            Console.WriteLine($"Supplier {name} removed successfully!");
        }

        private void ValidateExactParameterLength(string[] parameters, int length)
        {
            if (parameters.Length != length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be exactly {length}");
            }
        }

        private void ValidateMinimumParameterLength(string[] parameters, int length)
        {
            if (parameters.Length < length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be more than {length}");
            }
        }
    }
}