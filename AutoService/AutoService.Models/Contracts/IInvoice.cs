using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IInvoice
    {
        string Number { get; }
        decimal Amount { get; }
        decimal PaidAmount { get; }

        ICollection<ISell> InvoiceItem { get; }

        void IncreasePaidAmount(decimal amount);
        decimal GetOutstandingBalance();
    }
}