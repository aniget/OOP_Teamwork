using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Core.Validator;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Invoice : IInvoice
    {
        private readonly string number;
        private readonly DateTime date;
        private readonly IClient client;
        private decimal amount;
        private decimal paidAmount;
        private ICollection<ISell> invoiceItems;

        public Invoice(string number, DateTime date, IClient client)
        {
            Validate.StringForNullEmpty(number);
            Validate.InvoiceNumberLength(number.Length);

            this.number = number;

            Validate.CheckNullObject(client);

            this.client = client;

            this.date = date;

            this.invoiceItems = new List<ISell>();
        }

        public string Number { get => this.number; }

        public DateTime Date { get => this.date; }

        public decimal Amount
        {
            get => this.amount;
            private set
            {
                Validate.InvoicePositiveAmount(value);
                this.amount = value;
            }
        }

        public decimal PaidAmount
        {
            get => this.paidAmount;
            private set
            {
                Validate.InvoiceOverpaid(this.Amount, value);
                this.paidAmount = value;
            }
        }

        public IClient Client
        {
            get => this.client;
        }

        public ICollection<ISell> InvoiceItems
        {
            get => this.invoiceItems;
        }

        public void IncreasePaidAmount(decimal amount)
        {
            this.PaidAmount += amount;
        }

        public decimal GetOutstandingBalance()
        {
           return this.Amount - this.PaidAmount;
        }

        //TODO: Alex, please check it. Once I removed the price from the IWork (because we had a duplication in IWork and IStock) and the commented row below complained
        public void CalculateInvoiceAmount()
        {
            this.Amount = this.invoiceItems.Select(i => i.SellPrice).Sum();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Invoice amount: {this.Amount} $");
            sb.AppendLine($"Outstanding amount: {this.GetOutstandingBalance()} $");
            sb.AppendLine("Invoiced items:");

            foreach (var item in InvoiceItems)
            {
                ;
                sb.AppendLine("===" + Environment.NewLine + item + Environment.NewLine + "===");
            }

            return sb.ToString();
        }
    }
}
