using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrderStock : IOrder
    {
        IStock Stock { get; }
    }


}
