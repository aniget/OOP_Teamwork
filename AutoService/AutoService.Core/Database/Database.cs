using System.Collections.Generic;
using AutoService.Core.Contracts;
using AutoService.Models.Assets;
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
        }

        public IList<IEmployee> Employees { get; set; }

        public IList<BankAccount> BankAccounts { get; set; }
        
        public IList<ICounterparty> Clients { get; set; }
        
        public IList<ICounterparty> Suppliers { get; set; }
        
    }
}