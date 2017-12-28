using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Invoice : IInvoice
    {
        private readonly string number;
        private decimal amount;
        private decimal paidAmount;
        private ICollection<ISell> invoiceItems;
        private IClient client;

        public Invoice(string number, IClient client)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Invoice number cannot be null!");
            }
            
            this.number = number;
            this.client = client ?? throw new ArgumentException("Client cannot be null");
            this.invoiceItems = new List<ISell>();
        }

        public string Number { get => this.number; }

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

        public IClient Client => this.client;

        public ICollection<ISell> InvoiceItems
        {
            get => this.invoiceItems;
            set => this.invoiceItems = value;
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
                sb.AppendLine("===" + Environment.NewLine + item + "===");
            }

            return sb.ToString();
        }
    }
}
