using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public class Invoice : IInvoice
    {
        private string number;
        private decimal amount;
        private PaymentType paymentType;
        private decimal paidAmount;

        public Invoice(string number, decimal amount, PaymentType paymentType)
        {
            if (number.Length != 10)
            {
                throw new ArgumentException("Invoice length must be 10 symbols.");
            }
            this.number = number;
            this.amount = amount;

            if (paymentType != PaymentType.Bank || paymentType != PaymentType.Card || paymentType != PaymentType.Cash)
            {
                throw new ArgumentException("Invalid payment type!");
            }

            this.paymentType = paymentType;
        }

        public string Number => this.number;
        public decimal Amount => this.amount;
        public PaymentType PaymentType => this.paymentType;

        public decimal PaidAmount
        {
            get => this.paidAmount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Paid amount cannot be negative!");
                }
                this.paidAmount = value;
            }
        }

        public ICollection<ISell> InvoiceItem { get; }

        public void IncreasePaidAmount(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Paid amount cannot be negative!");
            }
            decimal paidAmount = this.paidAmount + amount;
            this.paidAmount = paidAmount;
        }

        public decimal GetOutstandingBalance()
        {
            decimal outstandingAmount = this.amount - this.paidAmount;
            return outstandingAmount;
        }
    }
}
