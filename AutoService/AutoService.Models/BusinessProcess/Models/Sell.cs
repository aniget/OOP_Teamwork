using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        private decimal sellPrice;
        private IClient client;
        private IVehicle vehicle;

        protected Sell(IEmployee responsibleEmployee, decimal sellPrice, IClient client, IVehicle vehicle)
            : base(responsibleEmployee, TypeOfWork.Selling)
        {
            Validate.CheckNullObject(new object[] { client, vehicle });
            Validate.SellPrice(sellPrice);

            this.client = client;
            this.sellPrice = sellPrice;
            this.vehicle = vehicle;
        }

        public decimal SellPrice => this.sellPrice;
        
        public IClient Client => this.client;
        
        public IVehicle Vehicle => this.vehicle;
        
        public abstract string AdditionalInfoForServiceType();
        public abstract string AdditionalInfoForSale();
        public abstract decimal GetSalePrice();

        public override string ToString()
        {
            return string.Format("Information about sale of {0} to client {1} {2}" + Environment.NewLine 
                + "Performed by: {3}",
                this.AdditionalInfoForServiceType(), this.Client.Name, this.AdditionalInfoForSale(), this.ResponsibleEmployee.FirstName + " " + this.ResponsibleEmployee.LastName);
        }
    }
}
