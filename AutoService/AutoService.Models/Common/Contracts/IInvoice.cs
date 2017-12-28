using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IInvoice
    {
        string Number { get; }
        decimal Amount { get; } // Calculated property based on invoiceItems
        decimal PaidAmount { get; }
        IClient Client { get; }

        ICollection<ISell> InvoiceItems { get; }

        void IncreasePaidAmount(decimal amount);
        decimal GetOutstandingBalance(); // Calculated property Amount - PaidAmount

        void CalculateInvoiceAmoint();
    }
}