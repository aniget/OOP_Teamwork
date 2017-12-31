using AutoService.Models.Assets;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellService : ISell
    {
        string ServiceName { get; }
        
        int DurationInMinutes { get; }

        //decimal SellPrice { get; }
    }
}
