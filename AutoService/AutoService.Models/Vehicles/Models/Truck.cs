using System;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Truck : Vehicle, IVehicle
    {
        private int weightAllowedInKilograms;

        public Truck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms)
            : base(model, make, registrationNumber, year, engine)
        {
            this.WeightAllowedInKilograms = weightAllowedInKilograms;
            this.VehicleType = VehicleType.Truck;
        }

        public virtual int WeightAllowedInKilograms
        {
            get => this.weightAllowedInKilograms;
            protected set
            {
                Validate.NonNegativeValue(value, "Weight in kgs.");

                if (value > 20000)
                {
                    throw new ArgumentException("Vehicle with weight capacity over 20,000 kgs does not exist!");
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
