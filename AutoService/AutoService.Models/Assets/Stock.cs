using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Assets
{
    public class Stock : Asset,  IStock
    {
        private readonly decimal purchasePrice;
        private ICounterparty supplier;

        private const int minLen = 3;
        private const int maxLen = 10;

        public Stock(string name, IEmployee responsibleEmployee, string uniqueNumber, decimal purchasePrice, ICounterparty supplier) : base(name, responsibleEmployee, uniqueNumber)
        {
            if (purchasePrice < 0 || purchasePrice > 1000000)
            {
                throw new ArgumentException("Purchase price cannot be negative or greater than 1mln!");
            }
            this.purchasePrice = purchasePrice;
            if (supplier == null)
            {
                throw new ArgumentException(/*"Null "*/);
            }
            this.supplier = supplier;

            if (UniqueNumber.Length < minLen || UniqueNumber.Length > maxLen) { throw new ArgumentException($"The stock unique number bust be between {minLen} and {maxLen} characters long!"); }

        }

        public decimal PurchasePrice
        {
            get => this.purchasePrice;
        }

        public ICounterparty Supplier
        {
            get => this.supplier;
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   $"  - Purchase price: {this.PurchasePrice}" + Environment.NewLine +
                   $"  - Purchased from: {this.Supplier.Name}" + Environment.NewLine;
        }
    }
}
