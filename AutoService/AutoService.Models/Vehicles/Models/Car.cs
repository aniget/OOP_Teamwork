using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Car : Vehicle
    {
        public Car(string model, string make, VehicleType vehicleType, IClient owner) : base(model, make, vehicleType, owner)
        {
            this.VehicleType = VehicleType.FourWheel;
        }
    }
}
