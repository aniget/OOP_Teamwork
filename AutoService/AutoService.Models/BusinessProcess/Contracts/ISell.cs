using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        IClient Client { get; }

        Vehicle Vehicle { get; }

       decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute);
    }
}
