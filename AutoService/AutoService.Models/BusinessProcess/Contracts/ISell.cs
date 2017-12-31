using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        ICounterparty Client { get; }

        IVehicle Vehicle { get; }

        void SellToClientVehicle(/*IEmployee responsibleEmployee, IClient client, IVehicle vehicle, */ISell sell, IStock stock);
        
    }
}
