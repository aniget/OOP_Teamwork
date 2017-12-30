using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Stock : Asset,  IStock
    {
        private readonly decimal purchasePrice;
        private ICounterparty supplier;

        public Stock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty supplier) : base(name, responsibleEmployee)
        {
            if (purchasePrice < 0 || purchasePrice > 1000000)
            {
                throw new ArgumentException("Purchase price cannot be negative or greater than 1mln!");
            }
            this.purchasePrice = purchasePrice;
            if (supplier == null)
            {
                throw new ArgumentException("Null ");
            }
            this.supplier = supplier;
        }

        public decimal PurchasePrice
        {
            get => this.purchasePrice;
        }

        public ICounterparty Supplier
        {
            get => this.supplier;
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
                   $"  - Purchased from: {this.Supplier.Name}" + Environment.NewLine;
        }
    }
}
