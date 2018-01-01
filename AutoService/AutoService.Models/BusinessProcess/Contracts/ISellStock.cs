using AutoService.Models.Assets.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellStock: ISell
    {
        IStock Stock { get; }
    }
}
