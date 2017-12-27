using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    class SellService : Sell, ISellService
    {
        //const declaration
        private const int minDuration = 10;
        private const int maxDuration = 2400; //5day @ 8 hours per day


        private int durationInMinutes;
        private string serviceName;


        public SellService(IEmployee responsibleEmployee, decimal price, TypeOfWork job, IClient client, Vehicle vehicle, string serviceName, int durationInMinutes) : base(responsibleEmployee, price, job, client, vehicle)
        {

            //validation of serviceName
            if (string.IsNullOrWhiteSpace(serviceName) || string.Empty== serviceName) {throw new ArgumentException("ServiceName cannot be null, empty or whitespace!"); }
            if (serviceName.Length < 5 || serviceName.Length < 500) { throw new ArgumentException("ServiceName should be between 5 and 500 characters long"); }

            //validation of durationInMinutes
            if (durationInMinutes < 0 ) { throw new ArgumentException("Duration of service should be provided in minutes and should be a positive number"); }
            if (durationInMinutes > 0 && durationInMinutes > minDuration) { throw new ArgumentException($"As per AutoService Policy minimum duration is {minDuration} min."); }
            if (durationInMinutes > maxDuration) { throw new ArgumentException($"Duration of service should be provided in minutes and should not exceed {maxDuration}min."); }

            this.serviceName = serviceName.Trim();
            this.durationInMinutes = durationInMinutes;

        }

        public string ServiceName => this.serviceName;
        public int DurationInMinutes => this.durationInMinutes;

    }
}
