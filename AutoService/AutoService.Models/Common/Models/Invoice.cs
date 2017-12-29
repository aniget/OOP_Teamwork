using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (string.IsNullOrWhiteSpace(number) || number.Length < 10)
            {
                throw new ArgumentException("Invoice number cannot be null!");
            }
            
            this.number = number;

            if (client == null)
            {
                throw new ArgumentException("Client cannot be null!");
            }

            this.client = client;

            this.date = date;

            this.invoiceItems = new List<ISell>();
        }

        public string Number { get => this.number; }

        public DateTime Date { get => this.date; }

        public decimal Amount
        {
            get => this.amount;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invoice amount must be positive!"); // to keep it simple no credit notes :)
                }
                this.amount = value;
            }
        }

        public decimal PaidAmount
        {
            get => this.paidAmount;
            protected set
            {
                if (this.Amount < value)
                {
                    throw new ArgumentException("Invoice is overpaid, please correct the amount to pay!");
                }
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

        public void CalculateInvoiceAmoint()
        {
            this.Amount = this.invoiceItems.Select(i => i.Price).Sum();
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
