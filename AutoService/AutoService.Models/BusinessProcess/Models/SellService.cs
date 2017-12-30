using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoService.Models.Assets;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;
using AutoService;

namespace AutoService.Models.BusinessProcess.Models
{
    public class SellService : Sell, ISellService
    {
        //const declaration
        private const int minDuration = 10;
        private const int maxDuration = 2400; //5day @ 8 hours per day


        private int durationInMinutes;
        private string serviceName;

        public SellService(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, /*IDictionary<IClient, ISell> notInvoicedSells, */string serviceName, int durationInMinutes)
            : base(responsibleEmployee, durationInMinutes * responsibleEmployee.RatePerMinute, TypeOfWork.Selling, client, vehicle/*, notInvoicedSells*/)
        {

            //validation of serviceName
            if (string.IsNullOrWhiteSpace(serviceName)) { throw new ArgumentException("ServiceName cannot be null, empty or whitespace!"); }
            if (serviceName.Length < 5 || serviceName.Length < 500) { throw new ArgumentException("ServiceName should be between 5 and 500 characters long"); }

            //validation of durationInMinutes
            if (durationInMinutes < 0) { throw new ArgumentException("Duration of service should be provided in minutes and should be a positive number"); }
            if (durationInMinutes > 0 && durationInMinutes > minDuration) { throw new ArgumentException($"As per AutoService Policy minimum duration is {minDuration} min."); }
            if (durationInMinutes > maxDuration) { throw new ArgumentException($"Duration of service should be provided in minutes and should not exceed {maxDuration}min."); }

            this.serviceName = serviceName.Trim();
            this.durationInMinutes = durationInMinutes;
        }


        public string ServiceName => this.serviceName;
        public int DurationInMinutes => this.durationInMinutes;

        public override void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, ISell sell)
        {
            base.SellToClientVehicle(responsibleEmployee, client, vehicle, this);
        }


        public override string AdditionalInfo_ServiceOrPart() { return "service";}

        //public override decimal CalculateRevenue() { return this.durationInMinutes * GetEmployeeRatePerMinute(ResponsibleEmployee);}

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   string.Format("The following service procedure was carried out: {0}" + Environment.NewLine
                                 + "and it took {1} minutes" + Environment.NewLine
                                 + "This service amounts to: {2} BGN"
                                 , this.ServiceName, this.durationInMinutes, GetEmployeeRatePerMinute(ResponsibleEmployee) * this.durationInMinutes);
        }
    }
}
