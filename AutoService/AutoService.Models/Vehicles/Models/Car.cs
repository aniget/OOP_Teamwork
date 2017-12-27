using System;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Car : Vehicle
    {
        private readonly int passengerCapacity;
        public Car(string model, string make, IClient owner, int passengerCapacity, string registrationNumber, string year, EngineType engine) : base(model, make, VehicleType.Car, owner, registrationNumber, year, engine)
        {
            if (passengerCapacity < 1 || passengerCapacity > 9)
            {
                throw new ArgumentException("Invalid passenger count!");
            }
            this.passengerCapacity = passengerCapacity;
        }

        public int PassengerCapacity { get => this.passengerCapacity; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   $"Passenger capacity: {this.PassengerCapacity} passenger(s)";
        }
    }
}
