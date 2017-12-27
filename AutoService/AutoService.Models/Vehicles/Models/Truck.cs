using System;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class Truck : Vehicle
    {
        private int weightAllowedInKilograms;

        public Truck(string model, string make, IClient owner, int weightAllowedInKilograms, string registrationNumber, string year, EngineType engine) : base(model, make, VehicleType.Truck, owner, registrationNumber, year, engine)
        {
           this.WeightAllowedInKilograms = weightAllowedInKilograms;
        }

        public virtual int WeightAllowedInKilograms
        {
            get => this.weightAllowedInKilograms;
            protected set
            {
                if (value <= 3500)
                {
                    throw new ArgumentException("Weight cannot be less than 3500 kilograms for trucks!");
                }
                this.weightAllowedInKilograms = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + 
                $"Maximum WeightCapacity: {this.WeightAllowedInKilograms} kgs";
        }
    }
}
