using System;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class SmallTruck : Truck
    {
        public SmallTruck(string model, string make, IClient owner, int weightAllowedInKilograms, string registrationNumber, string year, EngineType engine) : base(model, make, owner, weightAllowedInKilograms, registrationNumber, year, engine)
        {
        }

        public override int WeightAllowedInKilograms
        {
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid capacity!");
                }

                if (value > 3500)
                {
                    throw new ArgumentException("A Small truck cannot carry more than 3500 kilograms.");
                }
                base.WeightAllowedInKilograms = value;
            }
        }
    }
}
