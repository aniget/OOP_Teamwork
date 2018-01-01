using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Common.Contracts
{
    public interface IInvoice
    {
        string Number { get; }
        DateTime Date { get; }
        decimal Amount { get; } // Calculated property based on invoiceItems
        decimal PaidAmount { get; }
        IClient Client { get; }

        ICollection<ISell> InvoiceItems { get; }

        void IncreasePaidAmount(decimal amount);
        decimal GetOutstandingBalance(); // Calculated property Amount - PaidAmount

        void CalculateInvoiceAmount();
    }
}