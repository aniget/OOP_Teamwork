using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public abstract class Vehicle
    {
        private readonly string model;
        private readonly string make;
        private readonly VehicleType vehicleType;
        private readonly IClient owner;

        public Vehicle(string model, string make, VehicleType vehicleType, IClient owner)
        {
            //TODO Add validations in this constructor and fields readonly, because each vehicle will be only once assigned in our system. Even if a new owner buys it we will add it with another owner in order to keep history of previous ownership.
            this.model = model;
            this.make = make;
            this.vehicleType = vehicleType;
            this.owner = owner;
        }

        public string Model
        {
            get => this.model;
        }

        public string Make
        {
            get => this.make;
        }

        public VehicleType VehicleType
        {
            get => this.vehicleType;
        }

        public IClient Owner
        {
            get => this.owner;
        }
    }
}
