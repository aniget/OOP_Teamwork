using System;
using System.Text;
using AutoService.Core.Validator;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Models22222

{
    public abstract class Sell : Work, ISell
    {
        private readonly decimal sellPrice;


        protected Sell(IEmployee responsibleEmployee, decimal sellPrice, ICounterparty client)
            : base(responsibleEmployee, TypeOfWork.Selling)
        {
            Validate.CheckNullObject(new object[]{responsibleEmployee, client});


            this.Client = client ?? throw new ArgumentException("Client cannot be null");
            
            if (this.sellPrice < 0) { throw new ArgumentException("Sell price must be positive number"); }
            this.sellPrice = sellPrice;
        }

        public decimal SellPrice
        {
            get => this.sellPrice;
            protected set => this.sellPrice = value;
        }

        public ICounterparty Client { get; protected set; }
        public IVehicle Vehicle { get; protected set; }

        public virtual void SellToClientVehicle(ISell sell, IStock stock)
        {
            //remove from warehouse only when sell is of type ISellStock
            if (sell is ISellStock) { Warehouse.RemoveStockFromWarehouse(stock, this.ResponsibleEmployee, sell.Vehicle);}
        }

        private string SellToClientWithoutCar(IVehicle currentVehicle)
        {
            string vehicleMake = "";

            if (Vehicle == null) vehicleMake = "";
            else vehicleMake = Vehicle.Make + " " + Vehicle.Model;

            return "for vehicle " + vehicleMake;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Information about sale of {0} to client {1} {2}" + Environment.NewLine + "Performed by: {3}",
                AdditionalInfo_ServiceOrPart(), Client.Name, SellToClientWithoutCar(Vehicle), base.ResponsibleEmployee));
            return builder.ToString();
        }

        public abstract string AdditionalInfo_ServiceOrPart();

        //public abstract decimal CalculateRevenue();

    }
}
