using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellStock: ISell
    {
        IStock Stock { get; }
    }
}
