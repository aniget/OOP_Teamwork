using System.Collections.Generic;
using AutoService.Core.Contracts;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService
{
    public class Database : IDatabase
    {
        
        public Database()
        {
            this.Employees = new List<IEmployee>();
            this.BankAccounts = new List<BankAccount>();
            this.Clients = new List<ICounterparty>();
            this.Suppliers = new List<ICounterparty>();
            this.NotInvoicedSales = new Dictionary<IClient, IList<ISell>>();
            this.AvailableStocks = new List<IStock>();
        }

        public IList<IEmployee> Employees { get; set; }

        public IList<BankAccount> BankAccounts { get; set; }
        
        public IList<ICounterparty> Clients { get; set; }
        
        public IList<ICounterparty> Suppliers { get; set; }

        public Dictionary<IClient, IList<ISell>> NotInvoicedSales { get; set; }

        public IList<IStock> AvailableStocks { get; set; }
    }
}