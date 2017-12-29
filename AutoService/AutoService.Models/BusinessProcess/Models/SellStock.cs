using System;
using System.Collections.Generic;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    class SellStock: Sell, ISellStock
    {
        private readonly IStock stock;

        public SellStock(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, /*IDictionary<IClient, ISell> notInvoicedSells, */IStock stock) 
            : base(responsibleEmployee, stock.PurchasePrice * 1.2m, TypeOfWork.Selling, client, vehicle/*, notInvoicedSells*/)
        {
            this.stock = stock ?? throw new ArgumentException("Stock cannot be null");
        }

        public IStock Stock => this.stock;
        
        public override string AdditionalInfo_ServiceOrPart() { return "autoparts"; }

        //public override decimal CalculateRevenue() { return Stock.PurchasePrice * 1.5m;}

        public override void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, ISell sell)
        {
            base.SellToClientVehicle(responsibleEmployee, client, vehicle, this);
        }


        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   string.Format("The following part was sold: {0}" + Environment.NewLine
                                 + "This part costs: {1} BGN"
                       , this.Stock, this.Stock.PurchasePrice * 1.2m);
        }



    }
}
