using System.Collections.Generic;
using AutoService.Models.Models;

namespace AutoService.Models.Contracts
{
    public interface ICompany
    {
        string Name { get; }

        string Address { get; }

        string TaxNumber { get; }

        string UniqueNumber { get; }

        decimal CreditLimit { get; }

        ICollection<Invoice> Invoices { get; }

        void UpdateCreditLimit(decimal creditLimit);

        void ChangeAddress(string address);

        void ChangeTaxNumber(string number);
    }
}
