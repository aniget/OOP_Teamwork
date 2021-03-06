﻿using System;
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

        public IssueInvoices(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.modelValidator = processorLocator.GetProcessor <IValidateModel>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 1);

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
                    invoice.Amount = invoice.InvoiceItems.Select(i => i.SellPrice).Sum();
                }

                var clientToAddInvoice = this.database.Clients.FirstOrDefault(f => f.UniqueNumber == client.Key.UniqueNumber);
                clientToAddInvoice.Invoices.Add(invoice);
            }

            this.database.NotInvoicedSales.Clear();
            this.writer.Write($"{invoiceCount} invoices were successfully issued!");
        }
    }
}
