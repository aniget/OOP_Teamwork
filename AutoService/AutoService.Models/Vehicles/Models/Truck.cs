using System;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Truck : Vehicle
    {
        private int weightAllowedInKilograms;

        public Truck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms) : base(model, make, registrationNumber, year, engine)
        {
            this.WeightAllowedInKilograms = weightAllowedInKilograms;
            this.VehicleType = VehicleType.Truck;
        }

        public virtual int WeightAllowedInKilograms
        {
            get => this.weightAllowedInKilograms;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid weight!!");
                }
                this.weightAllowedInKilograms = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                $"-- Maximum WeightCapacity: {this.WeightAllowedInKilograms} kgs";
        }
    }
}
