using System;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        //ASSUMPTION: We assume that there are no two vehicles with the same make, model and registrationNumber

        private readonly string make;
        private readonly string model;
        private VehicleType vehicleType;
        private readonly string registrationNumber;
        private readonly string year;
        private readonly EngineType engine;

        public Vehicle(string make, string model, string registrationNumber,
            string year, EngineType engine)
        {
            Validate.StringForNullEmpty(make, model, registrationNumber, year);
            Validate.MakeAndModelLength(make, model);
            Validate.RegistrationNumber(registrationNumber);
            Validate.VehicleYear(year);

            this.model = model;
            this.make = make;
            this.registrationNumber = registrationNumber;
            this.year = year;
            this.engine = engine;
        }

        public string Make { get => this.make; }

        public string Model { get => this.model; }

        public VehicleType VehicleType { get => this.vehicleType; protected set { this.vehicleType = value; } }

        public string RegistrationNumber { get => this.registrationNumber; }

        public string Year { get => this.year; }

        public EngineType Engine { get => this.engine; }

        public override string ToString()
        {
            return $"Vehicle: {this.GetType().Name}" + Environment.NewLine +
                   $"-- Make: {this.Make}" + Environment.NewLine +
                   $"-- Model: {this.Model}" + Environment.NewLine +
                   $"-- Year: {this.Year}" + Environment.NewLine +
                   $"-- Engine: {this.Engine}" + Environment.NewLine +
                   $"-- Number of tires: {(int)this.VehicleType}" + Environment.NewLine +
                   $"-- Registration number: {this.RegistrationNumber}";
        }
    }
}
