using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.CustomExceptions;
using AutoService.Models.Validator;
using System;
using System.Collections.Generic;
using Autofac.Core.Registration;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        private readonly IList<IEmployee> employees;
        private readonly IList<IBankAccount> bankAccounts;
        private readonly IList<ICounterparty> clients;
        private readonly IList<ICounterparty> suppliers;
        private readonly IDictionary<IClient, IList<ISell>> notInvoicedSales;
        private readonly IStockManager stockManager;
        private readonly IEmployeeManager employeeManager;
        private readonly IInvoiceManager invoiceManager;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;
        private readonly IReader reader;

       private int lastInvoiceNumber = 0;
        private IAutoServiceFactory factory;

        //constructor
        public Engine
            (
            ICommandFactory commandFactory,
            IAutoServiceFactory autoServiceFactory,
            IDatabase database,
            IStockManager stockManager,
            IEmployeeManager employeeManager,
            IInvoiceManager invoiceManager,
            IValidateCore coreValidator,
            IValidateModel modelValidator,
            IWriter writer,
            IReader reader
            )
        {
            this.factory = autoServiceFactory;
            this.employees = database.Employees;
            this.bankAccounts = database.BankAccounts;
            this.clients = database.Clients;
            this.suppliers = database.Suppliers;
            this.notInvoicedSales = database.NotInvoicedSales;
            this.CommandFactory = commandFactory;
            this.stockManager = stockManager;
            this.coreValidator = coreValidator;
            this.employeeManager = employeeManager;
            this.invoiceManager = invoiceManager;
            this.writer = writer;
            this.reader = reader;

        }

        public ICommandFactory CommandFactory { get; }

        public void Run()
        {
            var inputLine = ReadCommand();
            var commandParameters = new string[] { string.Empty };


            while (inputLine != "exit")
            {

                commandParameters = ParseCommand(inputLine);

                try
                {
                    ICommand command = this.CommandFactory.CreateCommand(commandParameters[0]);
                    command.ExecuteThisCommand(commandParameters);
                }
                catch (NotSupportedException e) { this.writer.Write(e.Message); }
                catch (InvalidOperationException e) { this.writer.Write(e.Message); }
                catch (InvalidIdException e) { this.writer.Write(e.Message); }
                catch (ArgumentException e) { this.writer.Write(e.Message); }
                catch (ComponentNotRegisteredException e) { this.writer.Write($"There is no command named [{inputLine}] implemented! Please contact Dev team to implement it :)"); }

                this.writer.Write(Environment.NewLine + "<>-<>-<>-<>-<>-<>-<>-<>---<>-<>-<>-<>-<>-<>-<>-<>" + Environment.NewLine);
                this.writer.Write("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");

                inputLine = ReadCommand();

            }
        }

        private string ReadCommand()
        {
            return this.reader.Read();
        }

        private string[] ParseCommand(string command)
        {
            return command.Split(new string[] { ";" }, StringSplitOptions.None);
        }
    }
}