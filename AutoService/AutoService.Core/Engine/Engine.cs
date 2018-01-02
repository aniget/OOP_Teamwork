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
using AutoService.Models.Common;

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
            DepartmentType department;
            VehicleType vehicleType;
            EngineType engineType;
            IVehicle newVehicle = null;
            string assetName;
            string stockUniqueNumber;
            string bankAccountNumber;
            string employeeFirstName;
            string supplierUniqueNumber;
            string supplierUniqueName;
            string supplierAddress;
            string clientUniquieNumber;
            string clientUniqueName;
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

                    Validate.CounterpartyNotRegistered(this.clients, commandParameters[8], "client");

                    clientUniqueName = commandParameters[8];

                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    if (((IClient)client).Vehicles.Any(x => x.RegistrationNumber == vehicleRegistrationNumber))
                    {
                        throw new ArgumentException(
                            $"This client already has a vehicle with this registration number: {vehicleRegistrationNumber}.");
                    }
                    newVehicle = CreateVehicle(vehicleType, vehicleMake, vehicleModel, vehicleRegistrationNumber,
                        vehicleYear, engineType, additionalParams);

                    ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
                    Console.WriteLine(newVehicle);

                    Console.WriteLine($"Vehicle {vehicleMake} {vehicleModel} added to client {client.Name}");
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
                    if (stockExists == false)
                    {
                        throw new ArgumentException(
                            $"Trying to sell the stock with unique ID {stockUniqueNumber} that is not present in the Warehouse");
                    }
                    stock = this.warehouse.AvailableStocks.FirstOrDefault(x => x.UniqueNumber == stockUniqueNumber);

                    //no need to check vehicle for null because we create a default vehicle with every client registration
                    vehicle = ((IClient)client).Vehicles.FirstOrDefault(x =>
                       x.RegistrationNumber == vehicleRegistrationNumber);

                    this.SellStockToClient(stock, (IClient)client, vehicle);

                    break;

                case "sellServiceToClientVehicle":
                    //sellServiceToClientVehicle; Jo; 123456789; CA1234AC; Disk change; 240; Manarino; Management

                    //we can sell services to client without vehicle, e.g. client brings old tire rim for repair
                    Validate.EitherOrParameterLength(commandParameters, 6, 8);

                    employeeFirstName = commandParameters[1];
                    clientUniqueName = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    var serviceName = commandParameters[4];

                    int durationInMinutes = Validate.IntFromString(commandParameters[5], "duration in minutes");

                    Validate.EmployeeExist(this.employees, employeeFirstName);
                    employee = Validate.EmployeeUnique(this.employees, commandParameters, 1, 8);

                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    vehicle = ((IClient)client).Vehicles.FirstOrDefault(x =>
                       x.RegistrationNumber == vehicleRegistrationNumber);

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
                    //changeSupplierName; VintchetaBolchetaGaiki; VintchetaBolchetaGaikiNew
                    Validate.ExactParameterLength(commandParameters, 3);
                    supplierUniqueName = commandParameters[1];
                    var supplierNewUniqueName = commandParameters[2];
                    Validate.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");

                    this.ChangeCounterpartyName(supplierUniqueName, this.suppliers, supplierNewUniqueName);

                    Console.WriteLine($"{supplierUniqueName} changed sucessfully to {supplierNewUniqueName}");
                    break;

                case "removeSupplier":

                    Validate.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];

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
                    newVehicle = CreateVehicle(VehicleType.Car, "Empty", "Empty", "123456", "2000", EngineType.Petrol,
                        5);
                    ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
                    Console.WriteLine(newVehicle);

                    Console.WriteLine($"Default Vehicle added to client {client.Name}");

                    break;

                case "changeClientName":
                    //changeClientName; ClientSuperDuper; clientNewUniqueNameNew 
                    Validate.ExactParameterLength(commandParameters, 3);

                    clientUniqueName = commandParameters[1];
                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");

                    var clientNewUniqueName = commandParameters[2];
                    this.ChangeCounterpartyName(clientUniqueName, this.clients, clientNewUniqueName);
                    Console.WriteLine($"{clientUniqueName} name changed sucessfully to {clientNewUniqueName}");
                    break;

                case "removeClient":

                    Validate.ExactParameterLength(commandParameters, 2);

                    clientUniqueName = commandParameters[1];

                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    this.RemoveCounterparty(clientUniqueName, this.clients);

                    break;
                case "listWarehouseItems":
                    this.ListWarehouseItems();
                    break;
                case "addClientPayment":
                    //addClientPayment;<clientName>;<bankAccountId>;<invoiceNum>;<amount>
                    Validate.ExactParameterLength(commandParameters, 5);

                    clientUniqueName = commandParameters[1];
                    Validate.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(f => f.Name == clientUniqueName);

                    bankAccountId = Validate.IntFromString(commandParameters[2], "bankAccountId");
                    Validate.BankAccountById(this.bankAccounts, bankAccountId);
                    IInvoice invoiceFound = Validate.InvoiceExists(this.clients, client, commandParameters[3]);
                    decimal paymentAmount = Validate.DecimalFromString(commandParameters[4], "decimal");

                    this.AddClientPayment(invoiceFound, paymentAmount);

                    break;

                case "listClients":
                    this.ListClients();
                    break;
                case "withdrawCashFromBank":
                    //withdrawCashFromBank;<employeeId>;<bankAccountId>;<amount>

                    Validate.ExactParameterLength(commandParameters, 4);

                    Validate.BankAccountsCount(this.bankAccounts.Count);
                    employeeId = Validate.IntFromString(commandParameters[1], "employeeId");

                    employee = Validate.EmployeeById(this.employees, employeeId);

                    bankAccountId = Validate.IntFromString(commandParameters[2], "bankAccountId");

                    bankAccount = Validate.BankAccountById(this.bankAccounts, bankAccountId);

                    decimal withdrawAmount = Validate.DecimalFromString(commandParameters[3], "depositAmount");

                    this.WithdrawCashFromBank(bankAccount, withdrawAmount, employee);

                    break;
                case "help":
                    this.HelpThem();
                    break;

                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
        }

        private void WithdrawCashFromBank(BankAccount bankAccount, decimal withdrawAmount, IEmployee employee)
        {
            if (employee.Responsibilities.Contains(ResponsibilityType.Account) || employee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                bankAccount.WithdrawFunds(withdrawAmount);
                Console.WriteLine($"{withdrawAmount} BGN were successfully withdrawn by {employee.FirstName} {employee.LastName}");
            }
            else
            {
                throw new ArgumentException($"Employee {employee.FirstName} {employee.LastName} is not allowed to withdraw!");
            }
        }


        private void HelpThem()
        {
            Console.WriteLine("This is a sample AutoService software written for just 5 days." + Environment.NewLine +
                              "For suggestions on improvement please send email to holySynod@bg-patriarshia.bg" +
                              Environment.NewLine +
                              "Please donate in order to keep us alive! We accept BitCoin, LiteCoin, Ethereum, PitCoin , ShitCoin and any other coin!");
        }

        private void ListClients()
        {
            foreach (var client in this.clients)
            {
                Console.WriteLine(client);
            }
        }

        private void AddClientPayment(IInvoice invoiceFound, decimal paymentAmount)
        {
            invoiceFound.IncreasePaidAmount(paymentAmount);
            Console.WriteLine(
                $"amount {paymentAmount} successfully booked to invoice {invoiceFound.Number}. Thank you for your business!");
        }

        private void ListWarehouseItems()
        {
            Console.WriteLine(this.warehouse);
        }

        private void ChangeCounterpartyName(string counterpartyName, IList<ICounterparty> counterparties,
            string counterpartyNewName)
        {
            var supplier = counterparties.First(f => f.Name == counterpartyName);

            supplier.ChangeName(counterpartyNewName);
        }

        private void DepositCashInBankAccount(BankAccount bankAccount, decimal depositAmount)
        {
            bankAccount.DepositFunds(depositAmount);
            Console.WriteLine($"{depositAmount} BGN were successfully added to bank account {bankAccount.Name}");
        }

        private void CreateBankAccount(IEmployee employee, string assetName, DateTime currentAssetDate,
            string uniqueNumber)
        {
            if (employee.Responsibilities.Contains(ResponsibilityType.Account) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                BankAccount bankAccountToAdd =
                    this.factory.CreateBankAccount(assetName, employee, uniqueNumber, currentAssetDate);

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

        private IVehicle CreateVehicle(VehicleType vehicleType, string vehicleMake, string vehicleModel,
                string registrationNumber, string vehicleYear, EngineType engineType, int additionalParams)
        //vehicleType, vehicleMake, vehicleModel, registrationNumber, vehicleYear, engineType, additionalParams

        {
            IVehicle vehicle = null;

            if (vehicleType == VehicleType.Car)
            {
                vehicle = (IVehicle)this.factory.CreateVehicle(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams);
            }

            else if (vehicleType == VehicleType.SmallTruck)
            {
                vehicle = (IVehicle)this.factory.CreateSmallTruck(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams);
            }
            else if (vehicleType == VehicleType.Truck)
            {
                vehicle = (IVehicle)this.factory.CreateTruck(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams);
            }
            return vehicle;
        }

        private void RemoveResponsibilitiesToEmployee(IEmployee employee, string[] responsibilitiesToRemove)
        {
            var responsibilitesToRemove = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilitiesToRemove)
            {
                bool isValid = Validate.IsValidResponsibilityTypeFromString(responsibility);

                if (isValid)
                {
                    ResponsibilityType currentResponsibility;
                    Enum.TryParse(responsibility, out currentResponsibility);
                    responsibilitesToRemove.Add(currentResponsibility);
                }
            }
            employee.RemoveResponsibilities(responsibilitesToRemove);
        }

        private void AddResponsibilitiesToEmployee(IEmployee employee, string[] responsibilities)
        {
            var responsibilitesToAdd = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilities)
            {
                bool isValid = Validate.IsValidResponsibilityTypeFromString(responsibility);

                if (isValid)
                {
                    ResponsibilityType currentResponsibility;
                    Enum.TryParse(responsibility, out currentResponsibility);
                    responsibilitesToAdd.Add(currentResponsibility);
                }
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
            if (stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                IOrderStock orderStock = factory.CreateOrderStock(stock.ResponsibleEmployee, stock.Supplier, stock);
                this.warehouse.AddStockToWarehouse(stock, stock.ResponsibleEmployee);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required repsonsibilities to register asset {stock.Name}");
            }

            Console.WriteLine(
                $"{stock.Name} ordered from {stock.Supplier.Name} for the amount of {stock.PurchasePrice} are stored in the Warehouse." +
                Environment.NewLine +
                $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        private void SellStockToClient(IStock stock, IClient client, IVehicle vehicle)
        {
            ISell sellStock;
            if (stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                sellStock = factory.CreateSellStock(stock.ResponsibleEmployee, client, vehicle, stock);

                this.warehouse.RemoveStockFromWarehouse(stock, stock.ResponsibleEmployee);

                //sellStock.SellToClientVehicle(sellStock, stock);

                //record the Sell in the notInvoicedSells Dictionary
                AddSellToNotInvoicedItems(client, sellStock);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine(
                $"{stock.Name} purchased from {stock.Supplier.Name} was sold to {client.Name} for the amount of {sellStock.SellPrice}" +
                Environment.NewLine
                + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }

        private void SellServiceToClient(IEmployee responsibleEmployee, IClient client, IVehicle vehicle,
            string serviceName, int durationInMinutes)
        {
            ISellService sellService;
            if (responsibleEmployee.Responsibilities.Contains(ResponsibilityType.Repair) ||
                responsibleEmployee.Responsibilities.Contains(ResponsibilityType.SellService))
            {
                sellService = (ISellService)factory.CreateSellService(responsibleEmployee, client, vehicle,
                    serviceName, durationInMinutes);
                //sellService.SellToClientVehicle(sellService, null);

                //record the Sell in the notInvoicedSells Dictionary
                AddSellToNotInvoicedItems(client, sellService);

            }
            else
            {
                throw new ArgumentException(
                    $"Employee {responsibleEmployee.FirstName} {responsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            Console.WriteLine(
                $"{serviceName} was performed to {client.Name} for the amount of {sellService.SellPrice}" +
                Environment.NewLine
                + $"Employee responsible for the repair: {responsibleEmployee.FirstName} {responsibleEmployee.LastName}");
        }

        private void AddSellToNotInvoicedItems(IClient client, ISell sell)
        {
            if (!this.notInvoicedSells.ContainsKey(client))
            {
                this.notInvoicedSells[client] = new List<ISell>();
            }
            this.notInvoicedSells[client].Add(sell);
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
            foreach (var client in this.notInvoicedSells.OrderBy(o => o.Key.Name))
            {
                this.lastInvoiceNumber++;
                invoiceCount++;
                string invoiceNumber = this.lastInvoiceNumber.ToString();
                this.lastInvoiceDate = this.lastInvoiceDate.AddDays(3);
                IInvoice invoice = new Invoice(invoiceNumber, this.lastInvoiceDate, client.Key);

                foreach (var sell in client.Value)
                {
                    invoice.InvoiceItems.Add(sell);
                    invoice.CalculateInvoiceAmount();
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
                throw new ArgumentException(
                    "Supplier with the same unique number already exist. Please check the number and try again!");
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
                throw new ArgumentException(
                    $"Client with the same unique number {clientExisting.Name} already exists. Please check the number and try again!");
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