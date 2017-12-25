using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IClient
    {
        LegalType Legal { get; }

        string Name { get; }

        string Address { get; }

        string UniqueNumber { get; }

        ICollection<IInvoice> Invoices { get; }

        int DueDaysAllowed { get; }

        decimal Discount { get; }

        void UpdateDueDays(int dueDays);

    }
}
