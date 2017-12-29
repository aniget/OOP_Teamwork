using System;
using System.Collections.Generic;
using System.Linq;
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

        private IDictionary<IClient, ISell> notInvoicedSells;

        protected Sell(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty client, Vehicle vehicle, IDictionary<IClient, ISell> notInvoicedSells)
            : base(responsibleEmployee, price, job)
        {
            Client = client ?? throw new ArgumentException("Client cannot be null");
            Vehicle = vehicle ?? throw new ArgumentException("Vehicle cannot be null");
            this.notInvoicedSells = notInvoicedSells;
        }

        public ICounterparty Client { get; }
        public Vehicle Vehicle { get; }

        public virtual void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, ISell sell)
        {
            //employee is hired and has the responsibility to sell service
            if (responsibleEmployee == null) { throw new ArgumentException("Please enter employee!"); }
            if (responsibleEmployee.IsHired == false) { throw new ArgumentException($"Employee {responsibleEmployee} is no longer working for the AutoService!"); }
            if (responsibleEmployee.Responsibiities.Contains(ResponsibilityType.SellService) == false) { throw new ArgumentException($"Employee {responsibleEmployee} not authorized to repair vehicles"); }
            if (client == null) { throw new ArgumentException("Client cannot be null!"); }
            if (vehicle == null) { throw new ArgumentException("Vehicle cannot be null!"); }

            notInvoicedSells.Add(client, sell);
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Information about sale of {0} to client {1} for vehicle {2}" + Environment.NewLine + "Performed by: {3}",
                AdditionalInfo_ServiceOrPart(), Client.Name, Vehicle.Make + " " + Vehicle.Model, base.ResponsibleEmployee));
            return builder.ToString();
        }

        public abstract string AdditionalInfo_ServiceOrPart();

        //public abstract decimal CalculateRevenue();

    }
}
