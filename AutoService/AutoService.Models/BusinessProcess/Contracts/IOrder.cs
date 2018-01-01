using AutoService.Models.Common.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrder : IWork
    {
        ICounterparty Supplier { get; }
    }
}
