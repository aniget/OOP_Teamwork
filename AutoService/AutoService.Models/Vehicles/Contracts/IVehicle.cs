using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Contracts
{
    public interface IVehicle
    {
        string Model { get; }

        string Make { get; }

        VehicleType VehicleType { get; }

        string RegistrationNumber { get; }

        string Year { get; }

        EngineType Engine { get; }
    }
}