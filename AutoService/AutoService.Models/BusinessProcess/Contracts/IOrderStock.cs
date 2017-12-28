using AutoService.Models.Assets.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrderStock : IOrder
    {
        IStock Stock { get; }
    }
}
