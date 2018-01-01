using System;
using AutoService.Core.Validator;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Models22222

{
    public abstract class Sell : Work //, ISell
    {
        private decimal sellPrice;

        protected Sell(IEmployee responsibleEmployee, decimal sellPrice, IClient client, IVehicle vehicle)
            : base(responsibleEmployee, TypeOfWork.Selling)
        {
            Validate.CheckNullObject(new object[] {client, vehicle});
            Validate.SellPrice(sellPrice);

            this.client = client;
            this.sellPrice = sellPrice;
            this.vehicle = vehicle;
        }

        public decimal SellPrice
        {
            get => this.sellPrice; }

        public IClient Client
        {
            get => this.client;
        }

        public IVehicle Vehicle
        {
            get => this.vehicle;
        }

        public abstract string AdditionalInfoForServiceType();
        public abstract string AdditionalInfoForSale();
        public abstract decimal GetSalePrice();

        public override string ToString()
        {
           return string.Format("Information about sale of {0} to client {1} {2}" + Environment.NewLine + "Performed by: {3}",
               this.AdditionalInfoForServiceType(), this.Client.Name, this.AdditionalInfoForSale(), this.ResponsibleEmployee.FirstName + " " + this.ResponsibleEmployee.LastName);
        }
    }
}
