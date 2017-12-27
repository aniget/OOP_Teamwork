using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Models;
using AutoService.Models.Vehicles.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        //TODO: change type of vehicle from Vehicle to IVehicle once clarify with Alex what's going on
        protected Sell(IEmployee responsibleEmployee, decimal price, TypeOfWork job, IClient client, Vehicle vehicle) : base(responsibleEmployee, price, job)
        {
            Client = client;
            Vehicle = vehicle;
        }

        public IClient Client { get; }
        public Vehicle Vehicle { get; }

        public decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute)
        {
            throw new NotImplementedException();
        }

    }
}
