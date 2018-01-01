using System;
using AutoService.Models.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class SellStock : Sell, ISellStock
    {
        private readonly IStock stock;

        public SellStock(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, IStock stock)
            : base(responsibleEmployee, stock.PurchasePrice * 1.2m, client, vehicle)
        {
            Validate.CheckNullObject(stock);
            this.stock = stock;
        }

        public IStock Stock => this.stock;

        public override string AdditionalInfoForServiceType() { return "stock"; }

        public override string AdditionalInfoForSale() { return this.stock.Name; }

        public override decimal GetSalePrice()
        {
            return this.Stock.PurchasePrice * 1.2m * (1 - this.Client.Discount);
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   string.Format("The following part was sold: {0}" + Environment.NewLine
                                 + "This part costs: {1} BGN"
                       , this.Stock, this.SellPrice);
        }

    }
}
