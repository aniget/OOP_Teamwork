using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Assets;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        private decimal sellPrice;

        protected Sell(IEmployee responsibleEmployee, decimal sellPrice, TypeOfWork job, ICounterparty client, Vehicle vehicle)
            : base(responsibleEmployee, sellPrice, job)
        {
            Client = client ?? throw new ArgumentException("Client cannot be null");
            Vehicle = vehicle ?? throw new ArgumentException("Vehicle cannot be null");
            this.Job = TypeOfWork.Selling;

            if (this.sellPrice<0) { throw new ArgumentException("Sell price must be positive number"); }
            this.sellPrice = sellPrice;
        }

        public decimal SellPrice
        {
            get => this.sellPrice;
            protected set => this.sellPrice = value;
        }

        public ICounterparty Client { get; protected set; }
        public Vehicle Vehicle { get; protected set; }

        public virtual void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, ISell sell)
        {
            //employee is hired and has the responsibility to sell service
            if (responsibleEmployee == null) { throw new ArgumentException("Please enter employee!"); }
            if (responsibleEmployee.IsHired == false) { throw new ArgumentException($"Employee {responsibleEmployee} is no longer working for the AutoService!"); }
            if (responsibleEmployee.Responsibiities.Contains(ResponsibilityType.SellService) == false) { throw new ArgumentException($"Employee {responsibleEmployee} not authorized to repair vehicles"); }
            if (client == null) { throw new ArgumentException("Client cannot be null!"); }
            if (vehicle == null) { throw new ArgumentException("Vehicle cannot be null!"); }

            this.Client = client;
            this.Vehicle = vehicle;
            this.ResponsibleEmployee = responsibleEmployee;

            //notInvoicedSells.Add(client, sell);
        }

        private string SellToClientWithoutCar(IVehicle currentVehicle)
        {
            string vehicleMake = "";

            if (Vehicle == null)
            {
                vehicleMake = "";
            }
            else
            {
                vehicleMake = Vehicle.Make + " " + Vehicle.Model;
            }
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
