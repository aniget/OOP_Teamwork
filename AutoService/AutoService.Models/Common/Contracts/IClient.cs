using System.Collections.Generic;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Common.Contracts
{
    public interface IClient : ICounterparty
    {
        int DueDaysAllowed { get; }
        
        decimal Discount { get; }

        IList<Vehicle> Vehicles { get; }

        void AddVehicle(Vehicle vehicle);

        void RemoveVehicle(Vehicle vehicle);

        void UpdateDueDays(int dueDays);
    }
}
