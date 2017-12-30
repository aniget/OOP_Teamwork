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
using AutoService.Core.CustomExceptions;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        //TODO CHECK WHOLE DOCUMENT FOR REPETITIVE CODE

        private readonly IList<IEmployee> employees;
        private readonly IList<BankAccount> bankAccounts;
        private readonly IList<ICounterparty> clients;
        private readonly IList<ICounterparty> suppliers;
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
                catch (InvalidIdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine(Environment.NewLine +
                                  "<>-<>-<>-<>-<>-<>-<>-<>---<>-<>-<>-<>-<>-<>-<>-<>" +
                                  Environment.NewLine);
                command = ReadCommand();
            }
        }

        private string ReadCommand()
        {
            return Console.ReadLine();
        }

        private string[] ParseCommand(string command)
        {
            return command.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ExecuteSingleCommand(string[] commandParameters)
        {
            string commandType = string.Empty;

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
            decimal salary;
            //IOrderStock orderStock;
            IStock stock;
            string position;
            DepartmentType department;
            VehicleType vehicleType;
            EngineType engineType;
            string assetName;
            string supplierName;
            string employeeFirstName;
            string clientName;
            string supplierAddress;
            string supplierUniquieNumber;
            string vehicleMake;
            string vehicleModel;
            string registrationNumber;

            switch (commandType)
            {
                case "showEmployees":

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    this.ShowEmployees();
                    break;

                case "hireEmployee":

                    Validator.Validate.ExactParameterLength(commandParameters, 7);

                    var firstName = commandParameters[1];
                    var lastName = commandParameters[2];
                    position = commandParameters[3];

                    salary = Validator.Validate.DecimalFromString(commandParameters[4], "salary");

                    ratePerMinute = Validator.Validate.DecimalFromString(commandParameters[5], "ratePerMinute");

                    department = Validator.Validate.DepartmentTypeFromString(commandParameters[6], "department");

                    this.AddEmployee(firstName, lastName, position, salary, ratePerMinute, department);

                    break;

                case "fireEmployee":

                    Validator.Validate.ExactParameterLength(commandParameters, 2);

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    this.FireEmployee(employee);
                    break;

                case "changeEmployeeRate":

                    Validator.Validate.ExactParameterLength(commandParameters, 3);

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    ratePerMinute = Validator.Validate.DecimalFromString(commandParameters[2], "ratePerMinute");

                    this.ChangeRateOfEmployee(employee, ratePerMinute);
                    break;

                case "issueInvoices":
                    Validator.Validate.ExactParameterLength(commandParameters, 1);

                    this.IssueInvoices();
                    break;

                case "showAllEmployeesAtDepartment":

                    Validator.Validate.ExactParameterLength(commandParameters, 2);

                    department = Validator.Validate.DepartmentTypeFromString(commandParameters[1], "department");

                    this.ListEmployeesAtDepartment(department);
                    break;

                case "changeEmployeePosition":

                    Validator.Validate.ExactParameterLength(commandParameters, 3);

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    position = commandParameters[2];

                    this.ChangePositionOfEmployee(employee, position);
                    break;

                case "addEmployeeResponsibility":

                    Validator.Validate.MinimumParameterLength(commandParameters, 3);

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToAdd = commandParameters.Skip(2).ToArray();
                    this.AddResponsibilitiesToEmployee(employee, responsibilitiesToAdd);

                    break;

                case "removeEmpoloyeeResponsibility":

                    Validator.Validate.MinimumParameterLength(commandParameters, 3);

                    Validator.Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();

                    this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

                    break;

                case "addVehicleToClient":
                    //addVehicleToClient;Car;BMW;E39;CA1234AC;1999;Petrol;5;1
                    Validator.Validate.ExactParameterLength(commandParameters, 9);

                    vehicleType = Validator.Validate.VehicleTypeFromString(commandParameters[1], "vehicle type");
                    vehicleMake = commandParameters[2];
                    vehicleModel = commandParameters[3];
                    registrationNumber = commandParameters[4];
                    string vehicleYear = commandParameters[5];
                    engineType = Validator.Validate.EngineTypeFromString(commandParameters[6], "engine type");
                    var additionalParams = Validator.Validate.IntFromString(commandParameters[7], "additional parameters");
                    var clientId = Validator.Validate.IntFromString(commandParameters[8], "clientId");
                    var client = (Client)Validator.Validate.ClientById(this.clients, clientId);

                    if (client.Vehicles.Any(x => x.RegistrationNumber == registrationNumber))
                    {
                        throw new ArgumentException($"This client already has a vehicle with this registration number: {registrationNumber}.");
                    }
                    var newVehicle = CreateVehicle(vehicleType, vehicleMake, vehicleModel, registrationNumber, vehicleYear, engineType, additionalParams);
                    client.Vehicles.Add((Vehicle)newVehicle);

                    break;

                case "createBankAccount":

                    Validator.Validate.ExactParameterLength(commandParameters, 3);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "No employees currently in the service! You need to hire one then open the bank account :)");
                    }

                    employeeId = Validator.Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validator.Validate.EmployeeById(this.employees, employeeId);

                    assetName = commandParameters[2];

                    DateTime currentAssetDate = this.lastAssetDate.AddDays(5); //fixed date in order to check zero tests

                    this.CreateBankAccount(employee, assetName, currentAssetDate);
                    break;

                case "depositCashInBank":

                    Validator.Validate.ExactParameterLength(commandParameters, 3);

                    Validator.Validate.BankAccountsCount(this.bankAccounts.Count);

                    int bankAccountId = Validator.Validate.IntFromString(commandParameters[1], "bankAccountId");

                    BankAccount bankAccount = Validator.Validate.BankAccountById(this.bankAccounts, bankAccountId);

                    decimal depositAmount = Validator.Validate.DecimalFromString(commandParameters[2], "depositAmount");

                    this.DepositCashInBankAccount(bankAccount, depositAmount);
                    break;

                case "sellToClientVehicle":
                    //sellToClientVehicles;empl;cl;veh;sell
                    employeeFirstName = commandParameters[1];
                    clientName = commandParameters[2];
                    vehicleMake = commandParameters[3];
                    vehicleModel = commandParameters[4];
                    registrationNumber = commandParameters[5];

                    Validator.Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validator.Validate.EmployeeUnique(this.employees, commandParameters, 1, 7);

                    //validate client
                    //validate vehicle

                    this.SellStockToClient(stock);

                    break;



                case "orderStockToWarehouse":

                    Validator.Validate.EitherOrParameterLength(commandParameters, 5, 7);

                    employeeFirstName = commandParameters[1];
                    supplierName = commandParameters[2];
                    var stockName = commandParameters[3];
                    decimal purchasePrice = Validator.Validate.DecimalFromString(commandParameters[4], "purchasePrice");

                    Validator.Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validator.Validate.EmployeeUnique(this.employees, commandParameters, 1, 7);
                    
                    if (this.suppliers.Select(x => x.Name == supplierName).Count() > 1)
                    {
                        throw new ArgumentException("More than one registered supplier with same name, please provide unique number INSTEAD of name");
                    }
                    else if (!this.suppliers.Any(x => x.Name == supplierName))
                    {
                        throw new ArgumentException($"Our AutoService does not work with supplier {supplierName}");
                    }

                    supplier = this.suppliers.Single(x => x.Name == supplierName);

                    stock = new Stock(stockName, employee, purchasePrice, supplier);

                    //OrderStock orderStock = null;
                    ////this.OrderStockToday(orderedStockClass, emplFN, supplN);

                    this.OrderStockFromSupplier(stock);
                    break;

                case "registerSupplier":

                    Validator.Validate.ExactParameterLength(commandParameters, 4);

                    supplierName = commandParameters[1];
                    supplierAddress = commandParameters[2];
                    supplierUniquieNumber = commandParameters[3];

                    this.AddSupplier(supplierName, supplierAddress, supplierUniquieNumber);
                    break;

                case "removeSupplier":

                    Validator.Validate.ExactParameterLength(commandParameters, 2);

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

        private IVehicle CreateVehicle(VehicleType vehicleType, string vehicleModel, string vehicleMake, string registrationNumber, string vehicleYear, EngineType engineType, int additionalParams)
        //vehicleType, vehicleMake, vehicleModel, registrationNumber, vehicleYear, engineType, additionalParams

        {
            IVehicle vehicle = null;

            if (vehicleType == VehicleType.Car)
            {
                vehicle = (IVehicle)this.factory.CreateCar(vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, additionalParams);
            }

            else if (vehicleType == VehicleType.SmallTruck)
            {
                vehicle = (IVehicle)this.factory.CreateSmallTruck(vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, additionalParams);
            }
            else if (vehicleType == VehicleType.Truck)
            {
                vehicle = (IVehicle)this.factory.CreateTruck(vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, additionalParams);
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

            Console.WriteLine($"{stock.Name} ordered from {stock.Supplier.Name} for the amount of {stock.PurchasePrice} are stored in the Warehouse." + Environment.NewLine + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        //private void SellStockToClient(IStock stock)
        //{
        //    if (stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
        //        stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse) ||
        //        stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.Manage))
        //    {
        //        ISell sellStock = factory.CreateSellStock(stock.ResponsibleEmployee, stockstock.PurchasePrice, TypeOfWork.Ordering, stock.Supplier, stock);
        //        sellStock.SellToClientVehicle(stock.ResponsibleEmployee.FirstName, stock.Supplier.Name, Vehicle, stock);
        //    }
        //    else
        //    {
        //        throw new ArgumentException(
        //            $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
        //    }

        private void SellStockToClient(IStock stock)
        {
            if (stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse) ||
                stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.Manage))
            {
                ISell sellStock = factory.CreateSellStock(stock.ResponsibleEmployee, stockstock.PurchasePrice, TypeOfWork.Ordering, stock.Supplier, stock);
                sellStock.SellToClientVehicle(stock.ResponsibleEmployee.FirstName, stock.Supplier.Name, Vehicle, stock);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine($"{stock.Name} ordered for {stock.client.Supplier.Name} for the amount of {stock.PurchasePrice * 1.2} are sold to the customer {customer/*}." + Environment.NewLine + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
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
    }
}