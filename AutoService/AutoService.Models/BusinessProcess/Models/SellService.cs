using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class SellService : Sell, ISellService
    {
        //const declaration
        private const int minDuration = 10;
        private const int maxDuration = 2400; //5day @ 8 hours per day

        private readonly int durationInMinutes;
        private readonly string serviceName;

        public SellService(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName, int durationInMinutes)
            : base(responsibleEmployee, durationInMinutes * responsibleEmployee.RatePerMinute * (1 - client.Discount), client, vehicle)
        {
            Validate.StringForNullEmpty(serviceName);
            Validate.ServiceNameLength(serviceName);
            Validate.ServiceDurationInMinutes(durationInMinutes, minDuration, maxDuration);

            this.serviceName = serviceName.Trim();
            this.durationInMinutes = durationInMinutes;
        }

        public string ServiceName => this.serviceName;

        public int DurationInMinutes => this.durationInMinutes;

        public override string AdditionalInfoForServiceType() { return "service"; }

        public override string AdditionalInfoForSale() { return this.ServiceName; }

        public override decimal GetSalePrice()
        {
            return this.DurationInMinutes * this.ResponsibleEmployee.RatePerMinute * (1 - this.Client.Discount);
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   string.Format("The following service procedure was carried out: {0}" + Environment.NewLine
                                 + "and it took {1} minutes" + Environment.NewLine
                                 + "This service amounts to: {2} BGN"
                       , this.ServiceName, this.durationInMinutes, this.GetSalePrice());
        }
    }
}
