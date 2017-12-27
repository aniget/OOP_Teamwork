using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrderStock : IOrder
    {
        IStock Stock { get; }
    }
}
