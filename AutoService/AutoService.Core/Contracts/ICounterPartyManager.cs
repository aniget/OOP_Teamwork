using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Contracts
{
    public interface ICounterPartyManager
    {
        void SetCounterParty(ICounterparty client);

        void AddVehicle(Vehicle vehicle);

        void RemoveVehicle(Vehicle vehicle);

        void UpdateDueDays(int dueDays);

        void ChangeName(string name);
    }
}
