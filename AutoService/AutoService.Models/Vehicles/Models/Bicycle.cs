using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Bicycle : Vehicle
    {
        public Bicycle(string model, string make, VehicleType vehicleType, IClient owner) : base(model, make, vehicleType, owner)
        {
            this.vehicleType = VehicleType.TwoWheel;
        }
    }
}
