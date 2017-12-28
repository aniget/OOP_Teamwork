using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        ICounterparty Client { get; }

        Vehicle Vehicle { get; }

        void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string invoiceNumber,
            ISell sell);


    }
}
