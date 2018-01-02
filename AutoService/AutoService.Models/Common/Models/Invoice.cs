using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;

namespace AutoService.Models.Common.Models
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
            Validate.CheckNullObject(client);

            this.number = number;
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

        public void CalculateInvoiceAmount()
        {
            this.Amount = this.invoiceItems.Select(i => i.SellPrice).Sum();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Invoice amount: {this.Amount} BGN");
            sb.AppendLine($"Outstanding amount: {this.GetOutstandingBalance()} BGN");
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
