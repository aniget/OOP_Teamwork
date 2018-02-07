using System;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;

namespace AutoService.Core.Commands
{
    public class IssueInvoices : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;

        public IssueInvoices(IDatabase database, IValidateCore coreValidator, IValidateModel modelValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.modelValidator = modelValidator;
            this.writer = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 1);

            this.IssueInvoicesMethod();
        }
        private void IssueInvoicesMethod()
        {
            int invoiceCount = 0;
            foreach (var client in this.database.NotInvoicedSales.OrderBy(o => o.Key.Name))
            {
                this.database.LastInvoiceNumber++;
                invoiceCount++;
                string invoiceNumber = this.database.LastInvoiceNumber.ToString();
                this.database.LastInvoiceDate = this.database.LastInvoiceDate.AddDays(3);
                IInvoice invoice = new Invoice(invoiceNumber, this.database.LastInvoiceDate, client.Key, modelValidator);

                foreach (var sell in client.Value)
                {
                    invoice.InvoiceItems.Add(sell);
                    invoice.CalculateInvoiceAmount();
                }
                var clientToAddInvoice =
                    this.database.Clients.FirstOrDefault(f => f.UniqueNumber == client.Key.UniqueNumber);
                clientToAddInvoice.Invoices.Add(invoice);
            }

            this.database.NotInvoicedSales.Clear();
            this.writer.Write($"{invoiceCount} invoices were successfully issued!");
        }
    }
}
