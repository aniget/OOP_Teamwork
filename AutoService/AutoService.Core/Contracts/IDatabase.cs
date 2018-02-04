using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using System.Collections.Generic;

namespace AutoService.Core.Contracts
{
    public interface IDatabase
    {
        IList<IEmployee> Employees { get; }
        IList<BankAccount> BankAccounts { get; }
        IList<ICounterparty> Clients { get; }
        IList<ICounterparty> Suppliers { get; }

    }
}