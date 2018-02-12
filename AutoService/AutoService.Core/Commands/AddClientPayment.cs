using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using System;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class AddClientPayment : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public AddClientPayment(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }


        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 5);

            var clientUniqueName = commandParameters[1];
            this.coreValidator.CounterpartyNotRegistered(this.database.Clients, clientUniqueName, "client");
            var client = this.database.Clients.FirstOrDefault(f => f.Name == clientUniqueName);

            int bankAccountId = this.coreValidator.IntFromString(commandParameters[2], "bankAccountId");
            this.coreValidator.BankAccountById(this.database.BankAccounts, bankAccountId);

            IInvoice invoiceFound = this.coreValidator.InvoiceExists(this.database.Clients, client, commandParameters[3]);
            invoiceFound.PaidAmount += this.coreValidator.DecimalFromString(commandParameters[4], "decimal");

            this.writer.Write($"amount {invoiceFound.PaidAmount} successfully booked to invoice {invoiceFound.Number}. Thank you for your business!");
        }
    }
}
