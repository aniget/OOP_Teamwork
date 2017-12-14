using System.Collections.Generic;
using AutoService.Models.Models;

namespace AutoService.Models.Contracts
{
    public interface IVendor : ICompany
    {
        //ICollection<Invoice> Invoices { get; }

        void PayInvoices();
    }
}
