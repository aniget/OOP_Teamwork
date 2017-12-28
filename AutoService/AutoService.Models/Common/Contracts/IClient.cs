using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Contracts
{
    public interface IClient : ICounterparty
    {
        int DueDaysAllowed { get; }
        
        decimal Discount { get; }

        IList<Vehicle> Vehicles { get; }

        void AddVehicle(Vehicle vehicle);

        void RemoveVehicle(Vehicle vehicle);

        void UpdateDueDays(int dueDays); //optional
    }
}
