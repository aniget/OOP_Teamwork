using System.Collections.Generic;

namespace AutoService.Models.Common.Contracts
{
    public interface ICounterparty
    {
        string Name { get; }

        string Address { get; }

        string UniqueNumber { get; }

        ICollection<IInvoice> Invoices { get; }

        void ChangeName(string name);
    }
}
