using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Models22222
{
    public class SellStock : Sell, ISellStock
    {
        private readonly IStock stock;

        public SellStock(IEmployee responsibleEmployee, decimal sellPrice, IClient client, IStock stock)
            : base(responsibleEmployee, stock.PurchasePrice * 1.2m, client)
        {
            this.stock = stock;
        }
        public IStock Stock => this.stock;

        public string Name => this.Stock.Name;

        public override string AdditionalInfo_ServiceOrPart() { return "autoparts"; }

        public override void SellToClientVehicle(ISell sell, IStock stockSelling)
        {
            base.SellToClientVehicle(this, Stock);
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
