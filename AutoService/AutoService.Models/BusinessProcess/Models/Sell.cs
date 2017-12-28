using System;
using System.Text;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Assets;
using AutoService.Models.Vehicles.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        //TODO: change type of vehicle from Vehicle to IVehicle once clarify with Alex what's going on
        protected Sell(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty client, Vehicle vehicle)
            : base(responsibleEmployee, price, job)
        {
            Client = client;
            Vehicle = vehicle;
        }

        public ICounterparty Client { get; }
        public Vehicle Vehicle { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Information about sale of {0} to client {1} for vehicle {2}" + Environment.NewLine + "Performed by: {3}",
                AdditionalInfo_ServiceOrPart(), Client.Name, Vehicle.Make + " " + Vehicle.Model, base.ResponsibleEmployee));
            return builder.ToString();
        }

        public abstract string AdditionalInfo_ServiceOrPart();

        public abstract decimal CalculateRevenue();

    }
}
