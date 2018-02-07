using System;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
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
        Dictionary<IClient, IList<ISell>> NotInvoicedSales { get; }
        //IList<IStock> AvailableStocks { get; }

        DateTime LastInvoiceDate { get; set; }
        DateTime LastAssetDate { get; set; }
        int LastInvoiceNumber { get; set; }

    }
}