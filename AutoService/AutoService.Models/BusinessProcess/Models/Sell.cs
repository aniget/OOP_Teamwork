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
        public IClient Client { get; }
        public Vehicle Vehicle { get; }

        public decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute)
        {
            throw new NotImplementedException();
        }
    }
}
