using System;
using System.Collections.Generic;
using System.Globalization;
using AutoService.Core.Contracts;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService
{
    public class Database : IDatabase
    {
        private DateTime lastInvoiceDate;
        private DateTime lastAssetDate;
        private int lastInvoiceNumber;

        public Database()
        {
            this.Employees = new List<IEmployee>();
            this.BankAccounts = new List<BankAccount>();
            this.Clients = new List<ICounterparty>();
            this.Suppliers = new List<ICounterparty>();
            this.NotInvoicedSales = new Dictionary<IClient, IList<ISell>>();
            this.AvailableStocks = new List<IStock>();
            this.lastInvoiceDate = DateTime.ParseExact("2017-01-15", "yyyy-MM-dd", CultureInfo.InvariantCulture); //fixed for simplicity
            this.lastAssetDate = DateTime.ParseExact("2017-01-30", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.lastInvoiceNumber = 0;
        }

        public IList<IEmployee> Employees { get; }

        public IList<BankAccount> BankAccounts { get; }

        public IList<ICounterparty> Clients { get; }

        public IList<ICounterparty> Suppliers { get; }

        public Dictionary<IClient, IList<ISell>> NotInvoicedSales { get; }

        public IList<IStock> AvailableStocks { get; }

        public DateTime LastInvoiceDate
        {
            get => this.lastInvoiceDate;
            set
            {
                if (value < lastInvoiceDate)
                {
                    throw new ArgumentException("Invoice cannot be backdated!");
                }
                this.lastInvoiceDate = value;

            }
        }

        public int LastInvoiceNumber
        {
            get => this.lastInvoiceNumber;
            set
            {
                if (value < lastInvoiceNumber)
                {
                    throw new ArgumentException("Invoices cannot be backnumbered!");
                }
                this.lastInvoiceNumber = value;

            }
        }

        public DateTime LastAssetDate
        {
            get => this.lastAssetDate;
            set
            {
                if (value < lastAssetDate)
                {
                    throw new ArgumentException("Assets cannot be backdated!");
                }
                this.lastAssetDate = value;
            }
        }
    }
}