using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface ICounterparty
    {
        string Name { get; }

        string Address { get; }

        string UniqueNumber { get; }

        ICollection<IInvoice> Invoices { get; }
    }
}
