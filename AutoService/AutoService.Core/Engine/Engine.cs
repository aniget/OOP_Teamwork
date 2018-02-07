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
using AutoService.Core.Contracts;
using AutoService.Core.Validator;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        private readonly IList<IEmployee> employees;
        private readonly IList<BankAccount> bankAccounts;
        private readonly IList<ICounterparty> clients;
        private readonly IList<ICounterparty> suppliers;
        private readonly IDictionary<IClient, IList<ISell>> notInvoicedSales;
        private readonly IWarehouse warehouse;
        private readonly IStockManager stockManager;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter consoleWriter;
        private readonly IReader consoleReader;

        private DateTime lastInvoiceDate =
            DateTime.ParseExact("2017-01-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        private int lastInvoiceNumber = 0;
        private IAutoServiceFactory factory;

        //constructor
        public Engine
            (
            ICommandFactory commandFactory,
            IAutoServiceFactory autoServiceFactory,
            IDatabase database, IWarehouse warehouse,
            IStockManager stockManager,
            IValidateCore coreValidator,
            IValidateModel modelValidator,
            IWriter consoleWriter,
            IReader consoleReader
            )
        {
            this.factory = autoServiceFactory;
            this.employees = database.Employees;
            this.bankAccounts = database.BankAccounts;
            this.clients = database.Clients;
            this.suppliers = database.Suppliers;
            this.notInvoicedSales = database.NotInvoicedSales;
            this.warehouse = warehouse;
            this.CommandFactory = commandFactory;
            this.stockManager = stockManager;
            this.coreValidator = coreValidator;
            this.modelValidator = modelValidator;
            this.consoleWriter = consoleWriter;
            this.consoleReader = consoleReader;

        }

        public ICommandFactory CommandFactory { get; }

        public void Run()
        {
            var inputLine = ReadCommand();
            var commandParameters = new string[] { string.Empty };


            while (inputLine != "exit")
            {

                commandParameters = ParseCommand(inputLine);
                ICommand command = this.CommandFactory.CreateCommand(commandParameters[0]);

                try
                {
                    command.ExecuteThisCommand(commandParameters);
                }

                catch (NotSupportedException e) { this.consoleWriter.Write(e.Message); }
                catch (InvalidOperationException e) { this.consoleWriter.Write(e.Message); }
                catch (InvalidIdException e) { this.consoleWriter.Write(e.Message); }
                catch (ArgumentException e) { this.consoleWriter.Write(e.Message); }

                this.consoleWriter.Write(Environment.NewLine + "<>-<>-<>-<>-<>-<>-<>-<>---<>-<>-<>-<>-<>-<>-<>-<>" + Environment.NewLine);
                this.consoleWriter.Write("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
                this.consoleWriter.Write("   ");

                inputLine = ReadCommand();

            }
        }

        private string ReadCommand()
        {
            return this.consoleReader.Read();
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
                this.consoleWriter.Write("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=" + Environment.NewLine);
                commandType = commandParameters[0];

            }
            catch
            {
                //this catch the IndexOutOfRange Exception and throws the error message of the Default of the Switch
            }
            int employeeId;
            decimal ratePerMinute;
            IEmployee employee;
            //ICounterparty supplier;
            ICounterparty client;
            IVehicle vehicle;
            //IOrderStock orderStock;
            IStock stock;
            string position;
            //VehicleType vehicleType;
            //EngineType engineType;
            //IVehicle newVehicle = null;
            string stockUniqueNumber;
            string employeeFirstName;
            string employeeLastName = "";
            string employeeDepartment = "";
            string supplierUniqueName;
            //string clientUniquieNumber;
            string clientUniqueName;
            //string clientAddress;
            //string vehicleMake;
            //string vehicleModel;
            string vehicleRegistrationNumber;

            switch (commandType)
            {

                case "changeEmployeeRate":

                    this.coreValidator.ExactParameterLength(commandParameters, 3);

                    this.coreValidator.EmployeeCount(this.employees.Count);

                    employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

                    employee = this.coreValidator.EmployeeById(this.employees, employeeId);

                    ratePerMinute = this.coreValidator.DecimalFromString(commandParameters[2], "ratePerMinute");

                    this.ChangeRateOfEmployee(employee, ratePerMinute);
                    break;

                //case "showAllEmployeesAtDepartment":
                //    //showAllEmployeesAtDepartment;ServicesForClients

                //    ValidateModel.ExactParameterLength(commandParameters, 2);

                //    department = ValidateModel.DepartmentTypeFromString(commandParameters[1], "department");

                //    this.ListEmployeesAtDepartment(department);
                //    break;

                case "changeEmployeePosition":

                    this.coreValidator.ExactParameterLength(commandParameters, 3);

                    this.coreValidator.EmployeeCount(this.employees.Count);

                    employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

                    employee = this.coreValidator.EmployeeById(this.employees, employeeId);

                    position = commandParameters[2];

                    this.ChangePositionOfEmployee(employee, position);
                    break;

                case "addEmployeeResponsibility":

                    this.coreValidator.MinimumParameterLength(commandParameters, 3);

                    this.coreValidator.EmployeeCount(this.employees.Count);

                    employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

                    employee = this.coreValidator.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToAdd = commandParameters.Skip(2).ToArray();
                    this.AddResponsibilitiesToEmployee(employee, responsibilitiesToAdd);

                    break;

                case "removeEmpoloyeeResponsibility":

                    this.coreValidator.MinimumParameterLength(commandParameters, 3);

                    this.coreValidator.EmployeeCount(this.employees.Count);

                    employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

                    employee = this.coreValidator.EmployeeById(this.employees, employeeId);

                    var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();

                    this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

                    break;

                case "sellStockToClientVehicle":
                    //sellStockToClientVehicle; Jo; 123456789; CA1234AC; RT20134HP; Manarino; Management
                    this.coreValidator.EitherOrParameterLength(commandParameters, 5, 7);

                    employeeFirstName = commandParameters[1];
                    clientUniqueName = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    stockUniqueNumber = commandParameters[4];
                    employeeLastName = "";
                    string employeeDepartmentName = "";

                    if (commandParameters.Length == 7)
                    {
                        employeeLastName = commandParameters[5];
                        employeeDepartmentName = commandParameters[6];
                        employee = this.coreValidator.EmployeeUnique(this.employees, employeeFirstName, employeeLastName,
                            employeeDepartmentName);
                    }
                    else
                    {
                        employee = this.coreValidator.EmployeeUnique(this.employees, employeeFirstName, null, null);
                    }

                    this.coreValidator.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");

                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    //stock we sell must be present in the warehouse :)
                    bool stockExists = stockManager.ConfirmStockExists(stockUniqueNumber, employee/*, warehouse*/);
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
                    this.coreValidator.EitherOrParameterLength(commandParameters, 6, 8);

                    employeeFirstName = commandParameters[1];
                    clientUniqueName = commandParameters[2];
                    vehicleRegistrationNumber = commandParameters[3];
                    var serviceName = commandParameters[4];

                    int durationInMinutes = this.coreValidator.IntFromString(commandParameters[5], "duration in minutes");

                    if (commandParameters.Length == 8)
                    {
                        employeeLastName = commandParameters[6];
                        employeeDepartment = commandParameters[7];
                        employee = this.coreValidator.EmployeeUnique(this.employees, employeeFirstName, employeeLastName,
                            employeeDepartment);
                    }
                    else
                    {
                        employee = this.coreValidator.EmployeeUnique(this.employees, employeeFirstName, null, null);
                    }

                    this.coreValidator.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(x => x.Name == clientUniqueName);

                    vehicle = ((IClient)client).Vehicles.FirstOrDefault(x =>
                       x.RegistrationNumber == vehicleRegistrationNumber);

                    this.SellServiceToClient(employee, (IClient)client, vehicle, serviceName, durationInMinutes);

                    break;

                case "removeSupplier":

                    this.coreValidator.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];

                    this.coreValidator.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");
                    this.RemoveCounterparty(supplierUniqueName, this.suppliers);
                    break;

                case "addClientPayment":
                    //addClientPayment;<clientName>;<bankAccountId>;<invoiceNum>;<amount>
                    this.coreValidator.ExactParameterLength(commandParameters, 5);

                    clientUniqueName = commandParameters[1];
                    this.coreValidator.CounterpartyNotRegistered(this.clients, clientUniqueName, "client");
                    client = this.clients.FirstOrDefault(f => f.Name == clientUniqueName);

                    int bankAccountId = this.coreValidator.IntFromString(commandParameters[2], "bankAccountId");
                    this.coreValidator.BankAccountById(this.bankAccounts, bankAccountId);
                    IInvoice invoiceFound = this.coreValidator.InvoiceExists(this.clients, client, commandParameters[3]);
                    decimal paymentAmount = this.coreValidator.DecimalFromString(commandParameters[4], "decimal");

                    this.AddClientPayment(invoiceFound, paymentAmount);

                    break;

                

                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
        }

        private void AddClientPayment(IInvoice invoiceFound, decimal paymentAmount)
        {
            invoiceFound.IncreasePaidAmount(paymentAmount);
            Console.WriteLine(
                $"amount {paymentAmount} successfully booked to invoice {invoiceFound.Number}. Thank you for your business!");
        }

        

        private void ChangeCounterpartyName(string counterpartyName, IList<ICounterparty> counterparties,
            string counterpartyNewName)
        {
            var supplier = counterparties.First(f => f.Name == counterpartyName);

            supplier.ChangeName(counterpartyNewName);
        }


        private void RemoveResponsibilitiesToEmployee(IEmployee employee, string[] responsibilitiesToRemove)
        {
            var responsibilitesToRemove = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilitiesToRemove)
            {
                bool isValid = this.coreValidator.IsValidResponsibilityTypeFromString(responsibility);

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
                bool isValid = this.coreValidator.IsValidResponsibilityTypeFromString(responsibility);

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

        //private void ListEmployeesAtDepartment(DepartmentType department)
        //{
        //    var employeesInDepartment = this.employees.Where(x => x.Department == department).ToList();
        //    if (employeesInDepartment.Count == 0)
        //    {
        //        throw new ArgumentException($"The are no employees at department: {department}!");
        //    }

        //    StringBuilder str = new StringBuilder();

        //    str.AppendLine($"Employees at: {department} department:");
        //    var counter = 1;

        //    foreach (var employee in employeesInDepartment)
        //    {
        //        str.AppendLine($"{counter}. {employee.ToString()}");
        //        counter++;
        //    }
        //    Console.WriteLine(str.ToString());
        //}

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
                IOrderStock orderStock = factory.CreateOrderStock(stock.ResponsibleEmployee, stock.Supplier, stock, modelValidator);
                if (((Supplier)stock.Supplier).InterfaceIsAvailable)
                    stockManager.AddStockToWarehouse(stock/*, warehouse*/);
                else
                    stockManager.AddStockToWarehouse(stock, stock.ResponsibleEmployee/*, warehouse*/);
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
                sellStock = factory.CreateSellStock(stock.ResponsibleEmployee, client, vehicle, stock, modelValidator);

                stockManager.RemoveStockFromWarehouse(stock, stock.ResponsibleEmployee/*, warehouse*/);

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
                    serviceName, durationInMinutes, modelValidator);
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
            if (!this.notInvoicedSales.ContainsKey(client))
            {
                this.notInvoicedSales[client] = new List<ISell>();
            }
            this.notInvoicedSales[client].Add(sell);
        }




        //private void AddEmployee(string firstName, string lastName, string position, decimal salary,
        //    decimal ratePerMinute, DepartmentType department)
        //{
        //    IEmployee employee =
        //        this.factory.CreateEmployee(firstName, lastName, position, salary, ratePerMinute, department);

        //    this.employees.Add(employee);
        //    Console.WriteLine(employee);
        //    Console.WriteLine($"Employee {firstName} {lastName} added successfully with Id {this.employees.Count}");
        //}

        private void AddClient(string name, string address, string uniqueNumber)
        {
            if (clients.Any(x => x.UniqueNumber == uniqueNumber))
            {
                var clientExisting = this.clients.First(f => f.UniqueNumber == uniqueNumber);
                throw new ArgumentException(
                    $"Client with the same unique number {clientExisting.Name} already exists. Please check the number and try again!");
            }

            ICounterparty client = this.factory.CreateClient(name, address, uniqueNumber, modelValidator);

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

        //static void c_CriticalAmountReached(object sender, CriticalLimitReachedEventArgs e)
        //{
        //    Console.WriteLine("The threshold of {0} was reached at", e.CriticalLimit);

        //}
    }
}