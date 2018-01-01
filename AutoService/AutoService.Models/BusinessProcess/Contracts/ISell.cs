using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        IClient Client { get; }

        IVehicle Vehicle { get; }

        decimal SellPrice { get; }
    }
}
