using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Contracts
{
    public interface IBusiness : IClient
    {
        string Name { get; }

        string Address { get; }

        string UniqueNumber { get; }

        ICollection<IInvoice> Invoices { get; }

        ICollection<IExternalBusinessJob> ServicesProvided { get; }
    }
}
