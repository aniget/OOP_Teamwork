﻿using System;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public class SmallTruck : Truck, IVehicle
    {
        public SmallTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms) 
            : base(model, make, registrationNumber, year, engine, weightAllowedInKilograms)
        {
            this.VehicleType = VehicleType.SmallTruck;
            this.WeightAllowedInKilograms = weightAllowedInKilograms;
        }

        public override int WeightAllowedInKilograms
        {
            protected set
            {
               if (value > 3500)
                {
                    throw new ArgumentException("A Small truck cannot carry more than 3500 kilograms.");
                }
                base.WeightAllowedInKilograms = value;
            }
        }
    }
}
