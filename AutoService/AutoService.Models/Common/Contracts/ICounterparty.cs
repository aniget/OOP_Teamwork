using System.Collections.Generic;

namespace AutoService.Models.Common.Contracts
{
    public interface ICounterparty
    {
        string Name { get; set; }

        string Address { get; }

        string UniqueNumber { get; }

        ICollection<IInvoice> Invoices { get; }
    }
}
