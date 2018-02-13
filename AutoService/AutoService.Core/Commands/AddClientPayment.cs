using AutoService.Core.Contracts;
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

        public AddClientPayment(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();

            this.coreValidator = processorLocator.GetProcessor<IValidateCore>();
            this.database = processorLocator.GetProcessor<IDatabase>();
            this.writer = processorLocator.GetProcessor<IWriter>();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            if (commandParameters == null)
                throw new System.ArgumentNullException(nameof(commandParameters));
            
            
            this.coreValidator.ExactParameterLength(commandParameters, 5);

            var clientUniqueName = commandParameters[1];
            this.coreValidator.CounterpartyNotRegistered(this.database.Clients, clientUniqueName, "client");

            var client = this.database.Clients.FirstOrDefault(f => f.Name == clientUniqueName);

            int bankAccountId = coreValidator.IntFromString(commandParameters[2], "bankAccountId");
            coreValidator.BankAccountById(this.database.BankAccounts, bankAccountId);

            IInvoice invoiceFound = this.coreValidator.InvoiceExists(this.database.Clients, client, commandParameters[3]);
            invoiceFound.PaidAmount += this.coreValidator.DecimalFromString(commandParameters[4], "decimal");

            writer.Write($"amount {invoiceFound.PaidAmount} successfully booked to invoice {invoiceFound.Number}. Thank you for your business!");
        }
    }
}
