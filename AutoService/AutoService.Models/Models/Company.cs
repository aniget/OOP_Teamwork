using System;
using System.Collections.Generic;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public abstract class Company : ICompany
    {
        private string name;
        private string address;
        private string taxNumber;
        private string uniqueNumber;
        private decimal creditLimit;
        private ICollection<Invoice> invoices;
        
        // company might not have a tax number so it might not be provided
        protected Company(string name, string address, string uniqueNumber, decimal creditLimit)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Company name cannot be null or empty!");
            }
            this.name = name;
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Company address cannot be null or empty!");
            }

            this.address = address;

            if (string.IsNullOrWhiteSpace(uniqueNumber))
            {
                throw new ArgumentException("Unique identification number cannot be null or empty!");
            }
            this.uniqueNumber = uniqueNumber;

            if (creditLimit <= 0)
            {
                throw new ArgumentException("Credit limit must be positive!");
            }

            this.creditLimit = creditLimit;
            this.invoices = new List<Invoice>();
        }

        protected Company(string name, string address, string uniqueNumber, decimal creditLimit, string taxNumber)
            : this(name, address, uniqueNumber, creditLimit)
        {
            this.taxNumber = taxNumber;
        }


        public string Name { get { return this.name; } }
        public string Address { get { return this.address; } }
        public string TaxNumber { get { return this.taxNumber; } }
        public string UniqueNumber { get { return this.uniqueNumber; } }
        public decimal CreditLimit { get { return this.creditLimit; } }
        public ICollection<Invoice> Invoices { get { return this.invoices; } } // ReadOnlyCollection?

        public void UpdateCreditLimit(decimal creditLimit)
        {
            if (creditLimit < 0)
            {
                throw new ArgumentException("Credit limit cannot be negative!");
            }
            this.creditLimit = creditLimit;
        }

        public void ChangeAddress(string address)
        {
            this.address = address;
        }

        public void ChangeTaxNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Tax number provided is not valid!");
            }
            this.taxNumber = number;
        }
    }
}
        