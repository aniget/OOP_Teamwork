using System;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public class Invoice : IInvoice
    {
        private string number;
        private decimal amount;
        private PaymentType paymentType;

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

        public string Number { get { return this.number; } }
        public decimal Amount { get { return this.amount; } }
        public PaymentType PaymentType { get { return this.paymentType; } }
    }
}
