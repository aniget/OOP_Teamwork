using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public class Invoice : IInvoice
    {
        public string Number { get; }
        public decimal Amount { get; }
        public decimal PaidAmount { get; }
        public ICollection<ISell> InvoiceItems { get; }
        public void IncreasePaidAmount(decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal GetOutstandingBalance()
        {
            throw new NotImplementedException();
        }
    }
}
