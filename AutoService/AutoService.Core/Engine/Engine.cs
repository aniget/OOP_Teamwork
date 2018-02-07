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
            IEmployee employee;
            ICounterparty client;
            IVehicle vehicle;
            IStock stock;
            string position;
            string stockUniqueNumber;
            string employeeFirstName;
            string employeeLastName = "";
            string employeeDepartment = "";
            string supplierUniqueName;
            string clientUniqueName;
            string vehicleRegistrationNumber;

            switch (commandType)
            {
                case "removeSupplier":

                    this.coreValidator.ExactParameterLength(commandParameters, 2);

                    supplierUniqueName = commandParameters[1];

                    this.coreValidator.CounterpartyNotRegistered(this.suppliers, supplierUniqueName, "supplier");
                    this.RemoveCounterparty(supplierUniqueName, this.suppliers);
                    break;


                default:
                    throw new NotSupportedException("Command not supported yet! Please call IT Support or raise a TT");
            }
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


    }
}