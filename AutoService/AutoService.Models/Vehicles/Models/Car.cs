using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Car : Vehicle, ICar
    {
        public Car(string model, string make, VehicleType vehicleType, IClient owner) : base(model, make, vehicleType, owner)
        {
            this.VehicleType = VehicleType.FourWheel;
        }

        public BrandType Brand { get; }
        public string Year { get; }
        public EngineType Engine { get; }
        public string RegistrationNumber { get; }
    }
}
