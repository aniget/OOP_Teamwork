using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        IClient Client { get; }

        IVehicle Vehicle { get; }

        decimal SellPrice { get; }
    }
}
