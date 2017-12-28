using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Stock : Asset,  IStock
    {
        private readonly decimal purchasePrice;
        private ICounterparty vendor;

        public Stock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty vendor) : base(name, responsibleEmployee)
        {
            if (purchasePrice < 0)
            {
                throw new ArgumentException("Purchase price cannot be negative!");
            }
            this.purchasePrice = purchasePrice;
            if (vendor == null)
            {
                throw new ArgumentException("Null ");
            }
            this.vendor = vendor;
        }

        public decimal PurchasePrice
        {
            get => this.purchasePrice;
        }
        public ICounterparty Vendor
        {
            get => this.vendor;
        }

        protected override void ChangeResponsibleEmployee(IEmployee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Responsible employee can't be null!");
            }
            if (employee.Responsibiities.Contains(ResponsibilityType.BuyPartForClient) || employee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) || employee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse))
            {
                this.ResponsibleEmployee = employee;
            }
            else
            {
                throw new ArgumentException($"Employee cannot be responsible for asset {this.GetType().Name}");
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   $"  - Purchase price: {this.PurchasePrice}" + Environment.NewLine +
                   $"  - Purchased from: {this.Vendor.Name}" + Environment.NewLine;
        }
    }
}
