using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Common.Contracts
{
    public interface IInvoice
    {
        string Number { get; }
        DateTime Date { get; }
        decimal Amount { get; set; } 
        decimal PaidAmount { get; set; }
        IClient Client { get; }

        ICollection<ISell> InvoiceItems { get; }
        decimal GetOutstandingBalance();

    }
}