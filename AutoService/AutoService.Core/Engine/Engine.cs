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
using AutoService.Core.Validator;
using AutoService.Models.Common.Models;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        //TODO CHECK WHOLE DOCUMENT FOR REPETITIVE CODE

        private readonly IList<IEmployee> employees;
        private readonly IList<BankAccount> bankAccounts;
        private readonly IList<ICounterparty> clients;
        private readonly IList<ICounterparty> suppliers;
        private readonly IDictionary<IClient, IList<ISell>> notInvoicedSells;

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
            this.notInvoicedSells = new Dictionary<IClient, IList<ISell>>();
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
            return command.Split(new string[] { ";" }, StringSplitOptions.None);
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
                //this catch the IndexOutOfRange Exception and throws the error message of the Default of the Switch
            }
            int employeeId;
            decimal ratePerMinute;
            IEmployee employee;
            ICounterparty supplier;
            ICounterparty client;
            IVehicle vehicle;
            decimal salary;
            //IOrderStock orderStock;
            IStock stock;
            string position;
            string clientNameOrUniqueNumber;
            DepartmentType department;
            VehicleType vehicleType;
            EngineType engineType;
            IVehicle newVehicle = null;
            string assetName;
            string stockUniqueNumber;
            string bankAccountNumber;
            string supplierName;
            string employeeFirstName;
            string supplierAddress;
            string supplierUniqueNumber;
            string clientUniqueName = "";
            string clientAddress;
            string clientUniquieNumber;
            string vehicleMake;
            string vehicleModel;
            string vehicleRegistrationNumber;


            switch (commandType)
            {
                case "showEmployees":

                    Validate.EmployeeCount(this.employees.Count);

                    this.ShowEmployees();
                    break;

                case "hireEmployee":

                    Validate.ExactParameterLength(commandParameters, 7);

                    var firstName = commandParameters[1];
                    var lastName = commandParameters[2];
                    position = commandParameters[3];

                    salary = Validate.DecimalFromString(commandParameters[4], "salary");

                    ratePerMinute = Validate.DecimalFromString(commandParameters[5], "ratePerMinute");

                    department = Validate.DepartmentTypeFromString(commandParameters[6], "department");

                    this.AddEmployee(firstName, lastName, position, salary, ratePerMinute, department);

                    break;

                case "fireEmployee":

                    Validate.ExactParameterLength(commandParameters, 2);

                    Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    this.FireEmployee(employee);
                    break;

                case "changeEmployeeRate":

                    Validate.ExactParameterLength(commandParameters, 3);

                    Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    ratePerMinute = Validate.DecimalFromString(commandParameters[2], "ratePerMinute");

                    this.ChangeRateOfEmployee(employee, ratePerMinute);
                    break;

                case "issueInvoices":
                    Validate.ExactParameterLength(commandParameters, 1);

                    this.IssueInvoices();

                    break;
                case "showAllEmployeesAtDepartment":

                    Validate.ExactParameterLength(commandParameters, 2);

                    department = Validate.DepartmentTypeFromString(commandParameters[1], "department");

                    this.ListEmployeesAtDepartment(department);
                    break;

                case "changeEmployeePosition":

                    Validate.ExactParameterLength(commandParameters, 3);

                    Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    position = commandParameters[2];

                    this.ChangePositionOfEmployee(employee, position);
                    break;

                case "addEmployeeResponsibility":

                    Validate.MinimumParameterLength(commandParameters, 3);

                    Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToAdd = commandParameters.Skip(2).ToArray();
                    this.AddResponsibilitiesToEmployee(employee, responsibilitiesToAdd);

                    break;

                case "removeEmpoloyeeResponsibility":

                    Validate.MinimumParameterLength(commandParameters, 3);

                    Validate.EmployeeCount(this.employees.Count);

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();

                    this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

                    break;

                case "addVehicleToClient":
                    //addVehicleToClient;Car;BMW;E39;CA1234AC;1999;Petrol;5;TelerikAcademy
                    Validate.ExactParameterLength(commandParameters, 9);

                    vehicleType = Validate.VehicleTypeFromString(commandParameters[1], "vehicle type");
                    vehicleMake = commandParameters[2];
                    vehicleModel = commandParameters[3];
                    vehicleRegistrationNumber = commandParameters[4];
                    string vehicleYear = commandParameters[5];
                    engineType = Validate.EngineTypeFromString(commandParameters[6], "engine type");
                    var additionalParams = Validate.IntFromString(commandParameters[7], "additional parameters");
                    client = (Client)Validate.ClientByUniqueName(this.clients, clientUniqueName);

                    if (((IClient)client).Vehicles.Any(x => x.RegistrationNumber == vehicleRegistrationNumber))
                    {
                        throw new ArgumentException($"This client already has a vehicle with this registration number: {vehicleRegistrationNumber}.");
                    }
                    newVehicle = CreateVehicle(vehicleType, vehicleMake, vehicleModel, vehicleRegistrationNumber, vehicleYear, engineType, additionalParams);

                    ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
                    Console.WriteLine(newVehicle);

                    Console.WriteLine($"Vehicle {vehicleMake} {vehicleModel} add to client {client.Name}");
                    break;

                case "createBankAccount":

                    Validate.ExactParameterLength(commandParameters, 4);

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "No employees currently in the service! You need to hire one then open the bank account :)");
                    }

                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    assetName = commandParameters[2];

                    Validate.ValidateBankAccount(commandParameters[3]);
                    bankAccountNumber = commandParameters[3];

                    DateTime currentAssetDate = this.lastAssetDate.AddDays(5); //fixed date in order to check zero tests

                    this.CreateBankAccount(employee, assetName, currentAssetDate, bankAccountNumber);
                    break;

                case "depositCashInBank":

                    Validate.ExactParameterLength(commandParameters, 3);

                    Validate.BankAccountsCount(this.bankAccounts.Count);

                    int bankAccountId = Validate.IntFromString(commandParameters[1], "bankAccountId");

                    BankAccount bankAccount = Validate.BankAccountById(this.bankAccounts, bankAccountId);

                    decimal depositAmount = Validate.DecimalFromString(commandParameters[2], "depositAmount");

                    this.DepositCashInBankAccount(bankAccount, depositAmount);
                    break;

                case "sellStockToClientVehicle":
                    //sellStockToClientVehicle; Jo; 123456789; CA1234AC; RT20134HP; Manarino; Management
                    Validate.EitherOrParameterLength(commandParameters, 5, 7);

                    IClient clientWeSellStockTo;
                    employeeFirstName = commandParameters[1];
                    clientNameOrUniqueNumber = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    stockUniqueNumber = commandParameters[4];

                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 7);

                    clientWeSellStockTo = (IClient)Validate.CounterpartyByNameOrUniqueNumber(clientNameOrUniqueNumber, clients);

                    //stock we sell must be present in the warehouse :)
                    bool stockExists = Warehouse.ConfirmStockExists(stockUniqueNumber, employee);
                    if (stockExists == false) { throw new ArgumentException($"Trying to sell the stock with unique ID {stockUniqueNumber} that is not present in the Warehouse"); }
                    stock = Warehouse.Stocks.FirstOrDefault(x => x.UniqueNumber == stockUniqueNumber);

                    //no need to ckech vehicle for null because we create a default vehicle with every client registration
                    vehicle = clientWeSellStockTo.Vehicles.FirstOrDefault(x => x.RegistrationNumber == vehicleRegistrationNumber);

                    Warehouse.RemoveStockFromWarehouse(stock, employee, vehicle);
                    this.SellStockToClient(stock, clientWeSellStockTo, vehicle);

                    break;

                case "sellServiceToClientVehicle":
                    //sellServiceToClientVehicle; Jo; 123456789; CA1234AC; Disk change; 240; Manarino; Management

                    //we can sell services to client without vehicle, e.g. client brings old tire rim for repair
                    Validate.EitherOrParameterLength(commandParameters, 6, 8);

                    IClient clientWeSellServiceTo;
                    employeeFirstName = commandParameters[1];
                    clientNameOrUniqueNumber = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    var serviceName = commandParameters[4];

                    int durationInMinutes = Validate.IntFromString(commandParameters[5], "duration in minutes");

                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 8);

                    clientWeSellServiceTo = (IClient)Validate.CounterpartyByNameOrUniqueNumber(clientNameOrUniqueNumber, clients);

                    vehicle = clientWeSellServiceTo.Vehicles.FirstOrDefault(x => x.RegistrationNumber == vehicleRegistrationNumber);

                    this.SellServiceToClient(employee, clientWeSellServiceTo, vehicle, serviceName, durationInMinutes);

                    break;


                case "orderStockToWarehouse":
                    //orderStockToWarehouse;Jo;AXM-AUTO;Rotinger HighPerformance Brake Disks;RT20134HP;180;Manarino;Management

                    Validate.EitherOrParameterLength(commandParameters, 6, 8);

                    employeeFirstName = commandParameters[1];
                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 8);

                    var supplierNameOrUniqueNumber = commandParameters[2];
                    supplier = Validate.CounterpartyByNameOrUniqueNumber(supplierNameOrUniqueNumber, this.suppliers);

                    var stockName = commandParameters[3];

                    stockUniqueNumber = commandParameters[4];

                    decimal purchasePrice = Validate.DecimalFromString(commandParameters[5], "purchasePrice");

                    stock = new Stock(stockName, employee, stockUniqueNumber, purchasePrice, supplier);

                    this.OrderStockFromSupplier(stock);
                    break;

                case "registerSupplier":
                    //registerSupplier;AXM - AUTO;54 Yerusalim Blvd Sofia Bulgaria;211311577
                    Validate.ExactParameterLength(commandParameters, 4);

                    supplierName = commandParameters[1];
                    supplierAddress = commandParameters[2];
                    supplierUniqueNumber = commandParameters[3];

                    Validate.ExistingSupplierFromNameAndUniqueNumber(this.suppliers, supplierName, supplierUniqueNumber);

                    this.AddSupplier(supplierName, supplierAddress, supplierUniqueNumber);
                    Console.WriteLine("Supplier registered sucessfully");
                    break;

                case "changeSupplierName":
                    //changeSupplierName; 211311577; VintchetaBolchetaGaiki
                    Validate.ExactParameterLength(commandParameters, 3);

                    supplierUniqueNumber = commandParameters[1];
                    supplierName = commandParameters[2];

                    Validate.ExistingSupplierFromUniqueNumber(this.suppliers, supplierUniqueNumber);

                    this.ChangeSupplierName(supplierUniqueNumber, supplierName);
                    Console.WriteLine($"Supplier name changed sucessfully to {supplierName}");
                    break;

                case "removeSupplier":

                    Validate.EitherOrParameterLength(commandParameters, 2, 3);

                    supplierName = commandParameters[1];

                    if (commandParameters.Length == 3)
                    {
                        supplierUniqueNumber = commandParameters[2];
                        this.RemoveSupplier(supplierName, supplierUniqueNumber);
                    }
                    else
                    {
                        this.RemoveSupplier(supplierName);
                    }
                    break;

                case "registerClient":
                    //registerClient; TelerikAcademy; Mladost Blvd Sofia Bulgaria; 123456789
                    Validate.ExactParameterLength(commandParameters, 4);

                    clientUniqueName = commandParameters[1];
                    clientAddress = commandParameters[2];
                    clientUniquieNumber = commandParameters[3];

                    this.AddClient(clientUniqueName, clientAddress, clientUniquieNumber);

                    //add default car to the client
                    client = this.clients.FirstOrDefault(x => x.UniqueNumber == clientUniquieNumber);
                    newVehicle = CreateVehicle(VehicleType.Car, "Empty", "Empty", "123456", "2000", EngineType.Petrol, 5);
                    ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
                    Console.WriteLine(newVehicle);

                    Console.WriteLine("Default Vehicle added to client {client.Name}");

                    break;

                case "removeClient":

                    Validate.EitherOrParameterLength(commandParameters, 2, 3);

                    clientUniqueName = commandParameters[1];

                    if (commandParameters.Length == 3)
                    {
                        clientUniquieNumber = commandParameters[2];
                        this.RemoveClient(clientUniqueName, clientUniquieNumber);
                    }
                    else
                    {
                        this.RemoveClient(clientUniqueName);
                    }
                    break;

                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
        }

        private void ChangeSupplierName(string supplierUniqueNumber, string supplierName)
        {
            var supplier = this.suppliers.First(f => f.UniqueNumber == supplierUniqueNumber);

            supplier.ChangeName(supplierName);
        }

        private void DepositCashInBankAccount(BankAccount bankAccount, decimal depositAmount)
        {
            bankAccount.DepositFunds(depositAmount);
            Console.WriteLine($"{depositAmount} $ were successfully added to bank account {bankAccount.Name}");
        }

        private void CreateBankAccount(IEmployee employee, string assetName, DateTime currentAssetDate, string uniqueNumber)
        {
            if (employee.Responsibiities.Contains(ResponsibilityType.Account) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage))
            {
                BankAccount bankAccountToAdd = this.factory.CreateBankAccount(assetName, employee, uniqueNumber, currentAssetDate);

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

        private IVehicle CreateVehicle(VehicleType vehicleType, string vehicleMake, string vehicleModel, string registrationNumber, string vehicleYear, EngineType engineType, int additionalParams)
        //vehicleType, vehicleMake, vehicleModel, registrationNumber, vehicleYear, engineType, additionalParams

        {
            IVehicle vehicle = null;

            if (vehicleType == VehicleType.Car)
            {
                vehicle = (IVehicle)this.factory.CreateVehicle(vehicleModel, vehicleMake, registrationNumber, vehicleYear, engineType, additionalParams);
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
            var employeesInDepartment = this.employees.Where(x => x.Department == department).ToArray();
            if (employeesInDepartment.Length == 0)
            {
                throw new ArgumentException($"The are no employees at department: {department}!");
            }

            StringBuilder str = new StringBuilder();

            str.AppendLine($"Employees at: {department} department:");
            var counter = 1;

            foreach (var employee in employeesInDepartment)
            {
                str.AppendLine($"{counter}. {employee.ToString()}");
                counter++;
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
                IOrderStock orderStock = factory.CreateOrderStock(stock.ResponsibleEmployee, stock.Supplier, stock);
                Warehouse.AddStockToWarehouse(stock, stock.ResponsibleEmployee);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required repsonsibilities to register asset {stock.Name}");
            }

            Console.WriteLine($"{stock.Name} ordered from {stock.Supplier.Name} for the amount of {stock.PurchasePrice} are stored in the Warehouse." + Environment.NewLine + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        private void SellStockToClient(IStock stock, IClient client, IVehicle vehicle)
        {
            ISell sellStock;
            if (stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.Sell) ||
                stock.ResponsibleEmployee.Responsibiities.Contains(ResponsibilityType.Manage))
            {
                sellStock = factory.CreateSellStock(stock.ResponsibleEmployee, client, vehicle, stock);

                Warehouse.RemoveStockFromWarehouse(stock, stock.ResponsibleEmployee, vehicle);
                //sellStock.SellToClientVehicle(sellStock, stock);

                //record the Sell in the notInvoicedSells Dictionary
                if (!this.notInvoicedSells.ContainsKey(client))
                {
                    this.notInvoicedSells[client] = new List<ISell>();
                }
                this.notInvoicedSells[client].Add(sellStock);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine($"{stock.Name} purchased from {stock.Supplier.Name} was sold to {client.Name} for the amount of {sellStock.SellPrice}" + Environment.NewLine
                + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        private void SellServiceToClient(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName, int durationInMinutes)
        {
            ISellService sellService;
            if (responsibleEmployee.Responsibiities.Contains(ResponsibilityType.Repair) ||
                responsibleEmployee.Responsibiities.Contains(ResponsibilityType.SellService))
            {
                sellService = (ISellService)factory.CreateSellService(responsibleEmployee, client, vehicle, serviceName, durationInMinutes);
                //sellService.SellToClientVehicle(sellService, null);

                //record the Sell in the notInvoicedSells Dictionary
                if (!this.notInvoicedSells.ContainsKey(client))
                {
                    this.notInvoicedSells[client] = new List<ISell>();
                }
                this.notInvoicedSells[client].Add(sellService);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {responsibleEmployee.FirstName} {responsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine($"{serviceName} was performed to {client.Name} for the amount of {sellService.SellPrice}" + Environment.NewLine
                              + $"Employee responsible for the repair: {responsibleEmployee.FirstName} {responsibleEmployee.LastName}");
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
            foreach (var client in this.notInvoicedSells.OrderBy(o => o.Key.Name).Distinct())
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
            Validate.CheckNullObject(employee);
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
            //Console.WriteLine($"Supplier {name} added successfully with Id {this.suppliers.Count}!");
        }

        private void AddClient(string name, string address, string uniqueNumber)
        {
            if (clients.Any(x => x.UniqueNumber == uniqueNumber))
            {
                var clientExisting = this.clients.First(f => f.UniqueNumber == uniqueNumber);
                throw new ArgumentException($"Client with the same unique number {clientExisting.Name} already exists. Please check the number and try again!");
            }

            ICounterparty client = this.factory.CreateClient(name, address, uniqueNumber);

            this.clients.Add(client);
            Console.WriteLine(client);
            //Console.WriteLine($"Client {name} added successfully with Id {this.clients.Count}!");
        }

        private void RemoveSupplier(string supplierName)
        {
            ICounterparty supplierToBeRemoved = this.suppliers.FirstOrDefault(x => x.Name == supplierName);
            if (supplierToBeRemoved == null)
            {
                throw new ArgumentException($"Supplier with name {supplierName} cannot be removed - no such supplier found");
            }
            else if (this.suppliers.Where(x => x.Name == supplierName).Count() > 1)
            {
                throw new ArgumentException($"More than one supplier with name {supplierName} exists." + Environment.NewLine + "Please provide both name and unique number of the supplier you want to remove");
            }
            this.suppliers.Remove(supplierToBeRemoved);
            Console.WriteLine(supplierToBeRemoved);
            Console.WriteLine($"Supplier {supplierName} removed successfully!");
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

        private void RemoveClient(string name)
        {
            ICounterparty clientToBeRemoved = this.clients.FirstOrDefault(x => x.Name == name);
            if (clientToBeRemoved == null)
            {
                throw new ArgumentException($"Client with name {name} cannot be removed - no such client found!");
            }
            else if (this.clients.Select(x => x.Name == name).Count() > 1)
            {
                throw new ArgumentException($"More than one client with name {name} exist." + Environment.NewLine + "Please provide both name and unique number");
            }
            this.clients.Remove(clientToBeRemoved);
            Console.WriteLine(clientToBeRemoved);
            Console.WriteLine($"Client {name} removed successfully!");
        }

        private void RemoveClient(string name, string uniqueNumber)
        {
            ICounterparty clientToBeRemoved = this.clients.FirstOrDefault(x => x.Name == name && x.UniqueNumber == uniqueNumber);
            if (clientToBeRemoved == null)
            {
                throw new ArgumentException($"Client with name {name} and uniquie number {uniqueNumber} cannot be removed - no such client found");
            }
            this.clients.Remove(clientToBeRemoved);
            Console.WriteLine(clientToBeRemoved);
            Console.WriteLine($"Supplier {name} removed successfully!");
        }
    }
}