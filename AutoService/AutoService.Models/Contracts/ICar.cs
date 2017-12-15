using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface ICar
    {
        BrandType Brand { get; }

        string Year { get; }

        EngineType Engine { get; }

        string RegistrationNumber { get; }

        void RepairCar();

    }
}
