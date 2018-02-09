using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class AddClientPayment : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;
        private readonly IInvoiceManager invoiceManager;

        public AddClientPayment(IDatabase database, IValidateCore coreValidator, IWriter writer, IInvoiceManager invoiceManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.invoiceManager = invoiceManager;
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
            decimal paymentAmount = this.coreValidator.DecimalFromString(commandParameters[4], "decimal");

            this.Add(invoiceFound, paymentAmount, invoiceManager);
        }

        private void Add(IInvoice invoiceFound, decimal paymentAmount, IInvoiceManager invoiceManager)
        {
            invoiceManager.SetInvoice(invoiceFound);
            invoiceManager.IncreasePaidAmount(paymentAmount);
            this.writer.Write(
                $"amount {paymentAmount} successfully booked to invoice {invoiceFound.Number}. Thank you for your business!");
        }
    }
}
