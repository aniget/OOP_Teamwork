using System;
using System.Collections.Generic;
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
        private readonly IValidateModel modelValidator;

        public Invoice(string number, DateTime date, IClient client, IValidateModel modelValidator)
        {
            this.modelValidator = modelValidator;
            this.ModelValidator.StringForNullEmpty(number);
            this.ModelValidator.CheckNullObject(client);

            this.number       = number;
            this.client       = client;
            this.date         = date;
            this.invoiceItems = new List<ISell>();
        }

        public string Number { get => this.number; }

        public DateTime Date { get => this.date; }

        public IValidateModel ModelValidator
        {
            get => this.modelValidator;
        }

        public decimal Amount
        {
            get => this.amount;
            set
            {
                this.ModelValidator.InvoicePositiveAmount(value);
                this.amount = value;
            }
        }

        public decimal PaidAmount
        {
            get => this.paidAmount;
            set
            {
                this.ModelValidator.InvoiceOverpaid(this.Amount, value);
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

        public decimal GetOutstandingBalance()
        {
            return this.Amount - this.PaidAmount;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Invoice amount: {this.Amount} BGN");
            sb.AppendLine($"Outstanding amount: {this.GetOutstandingBalance()} BGN");
            sb.AppendLine("Invoiced items:");

            foreach (var item in this.InvoiceItems)
            {
                sb.AppendLine("===" + Environment.NewLine + item + Environment.NewLine + "===");
            }

            return sb.ToString();
        }
    }
}
