using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    public class ExternalJob : Job, IExternalJob
    {
        public Employee Employee { get; }

        public IClient Client { get; }

        public decimal PricePerMinute { get; }

        public decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute)
        {
            throw new System.NotImplementedException();
        }
    }
}
