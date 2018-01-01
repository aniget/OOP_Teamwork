using AutoService.Core.Factory;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.CustomExceptions;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;
using AutoService.Models.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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
        private readonly Warehouse warehouse;

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
            this.warehouse = new Warehouse();
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
                Console.WriteLine();
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
            string employeeFirstName;
            string supplierUniqueNumber;
            string supplierUniqueName = "";
            string supplierAddress;
            string clientUniquieNumber;
            string clientUniqueName = "";
            string clientAddress;
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
                    clientUniqueName = commandParameters[8];
                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

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

                    employeeFirstName = commandParameters[1];
                    clientUniqueName = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    stockUniqueNumber = commandParameters[4];

                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 7);

                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");

                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    //stock we sell must be present in the warehouse :)
                    bool stockExists = this.warehouse.ConfirmStockExists(stockUniqueNumber, employee);
                    if (stockExists == false) { throw new ArgumentException($"Trying to sell the stock with unique ID {stockUniqueNumber} that is not present in the Warehouse"); }
                    stock = this.warehouse.AvailableStocks.FirstOrDefault(x => x.UniqueNumber == stockUniqueNumber);

                    //no need to ckech vehicle for null because we create a default vehicle with every client registration
                    vehicle = ((IClient)client).Vehicles.FirstOrDefault(x => x.RegistrationNumber == vehicleRegistrationNumber);

                    this.warehouse.RemoveStockFromWarehouse(stock, employee, vehicle);
                    this.SellStockToClient(stock, (IClient)client, vehicle);

                    break;

                case "sellServiceToClientVehicle":
                    //sellServiceToClientVehicle; Jo; 123456789; CA1234AC; Disk change; 240; Manarino; Management

                    //we can sell services to client without vehicle, e.g. client brings old tire rim for repair
                    Validate.EitherOrParameterLength(commandParameters, 6, 8);

                    employeeFirstName = commandParameters[1];
                    clientNameOrUniqueNumber = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    var serviceName = commandParameters[4];

                    int durationInMinutes = Validate.IntFromString(commandParameters[5], "duration in minutes");

                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 8);

                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    vehicle = ((IClient)client).Vehicles.FirstOrDefault(x => x.RegistrationNumber == vehicleRegistrationNumber);

                    this.SellServiceToClient(employee, (IClient)client, vehicle, serviceName, durationInMinutes);

                    break;


                case "orderStockToWarehouse":
                    //orderStockToWarehouse;Jo;AXM-AUTO;Rotinger HighPerformance Brake Disks;RT20134HP;180;Manarino;Management

                    Validate.EitherOrParameterLength(commandParameters, 6, 8);

                    employeeFirstName = commandParameters[1];
                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 8);

                    supplierUniqueName = commandParameters[2];

                    Validate.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");
                    supplier = this.suppliers.FirstOrDefault(x => x.Name == supplierUniqueName);

                    var stockName = commandParameters[3];

                    stockUniqueNumber = commandParameters[4];

                    decimal purchasePrice = Validate.DecimalFromString(commandParameters[5], "purchasePrice");

                    stock = new Stock(stockName, employee, stockUniqueNumber, purchasePrice, supplier);

                    this.OrderStockFromSupplier(stock);
                    break;

                case "registerSupplier":
                    //registerSupplier;AXM - AUTO;54 Yerusalim Blvd Sofia Bulgaria;211311577
                    Validate.ExactParameterLength(commandParameters, 4);

                    supplierUniqueName = commandParameters[1];
                    supplierAddress = commandParameters[2];
                    supplierUniqueNumber = commandParameters[3];

                    Validate.CounterpartyAlreadyRegistered(this.suppliers, supplierUniqueName, "supplier");

                    this.AddSupplier(supplierUniqueName, supplierAddress, supplierUniqueNumber);
                    Console.WriteLine("Supplier registered sucessfully");
                    break;

                case "changeSupplierName":
                    //changeSupplierName; VintchetaBolchetaGaiki
                    Validate.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];

                    Validate.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");

                    //this.ChangeCounterpartyName(supplierUniqueName, this.suppliers);
                    Console.WriteLine($"Supplier name changed sucessfully to {supplierUniqueName}");
                    break;

                case "removeSupplier":

                    Validate.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];
                    supplierUniqueNumber = commandParameters[2];

                    Validate.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");
                    this.RemoveCounterparty(supplierUniqueName, this.suppliers);
                    break;

                case "registerClient":

                    Validate.ExactParameterLength(commandParameters, 4);

                    clientUniqueName = commandParameters[1];
                    clientAddress = commandParameters[2];
                    clientUniquieNumber = commandParameters[3];

                    Validate.CounterpartyAlreadyRegistered(this.clients, clientUniqueName, "client");
                    this.AddClient(clientUniqueName, clientAddress, clientUniquieNumber);

                    //add default car to the client
                    client = this.clients.FirstOrDefault(x => x.UniqueNumber == clientUniquieNumber);
                    newVehicle = CreateVehicle(VehicleType.Car, "Empty", "Empty", "123456", "2000", EngineType.Petrol, 5);
                    ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
                    Console.WriteLine(newVehicle);

                    Console.WriteLine("Default Vehicle added to client {client.Name}");

                    break;

                case "changeClientName":
                    //changeClientName; ClientSuperDuper
                    Validate.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];
                    Validate.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");

                    //this.ChangeCounterpartyName(supplierUniqueName, this.suppliers);
                    Console.WriteLine($"Supplier name changed sucessfully to {supplierUniqueName}");
                    break;

                case "removeClient":

                    Validate.EitherOrParameterLength(commandParameters, 2, 3);

                    clientUniqueName = commandParameters[1];

                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    this.RemoveCounterparty(clientUniqueName, this.clients);

                    break;

                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
        }

        private void ChangeCounterpartyName(string counterpartyName, List<ICounterparty> counterparties)
        {
            var supplier = counterparties.First(f => f.Name == counterpartyName);

            supplier.ChangeName(counterpartyName);
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
                    Console.WriteLine($"Responsibility {responsibility} not valid!");
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
                this.warehouse.AddStockToWarehouse(stock, stock.ResponsibleEmployee);
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

                this.warehouse.RemoveStockFromWarehouse(stock, stock.ResponsibleEmployee, vehicle);
                //sellStock.SellToClientVehicle(sellStock, stock);

                //record the Sell in the notInvoicedSells Dictionary
                AddSellToNotInvoicedItems(client, sellStock);
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
                AddSellToNotInvoicedItems(client, sellService);

            }
            else
            {
                throw new ArgumentException(
                    $"Employee {responsibleEmployee.FirstName} {responsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine($"{serviceName} was performed to {client.Name} for the amount of {sellService.SellPrice}" + Environment.NewLine
                              + $"Employee responsible for the repair: {responsibleEmployee.FirstName} {responsibleEmployee.LastName}");
        }

        private void AddSellToNotInvoicedItems(IClient client, ISell sellService)
        {
            if (!this.notInvoicedSells.ContainsKey(client))
            {
                this.notInvoicedSells[client] = new List<ISell>();
            }
            this.notInvoicedSells[client].Add(sellService);
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

        private void RemoveCounterparty(string counterpartyUniqueName, IList<ICounterparty> counterparties)
        {
            ICounterparty counterparty = counterparties.FirstOrDefault(x => x.Name == counterpartyUniqueName);
            counterparties.Remove(counterparty);
            Console.WriteLine($"{counterpartyUniqueName} removed successfully!");
        }
    }
}
