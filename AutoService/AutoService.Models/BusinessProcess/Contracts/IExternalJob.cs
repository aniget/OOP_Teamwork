using AutoService.Models.Contracts;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IExternalJob : IJob
    {
        Employee Employee { get; }
        IClient Client { get; }

        decimal PricePerMinute { get; }

        decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute);
    }
}
