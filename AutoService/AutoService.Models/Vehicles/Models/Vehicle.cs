using System;
using System.Linq;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Models.Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private readonly string model;
        private readonly string make;
        private VehicleType vehicleType;
        //private readonly IClient owner;
        private readonly string registrationNumber;
        private readonly string year;
        private readonly EngineType engine;

        public Vehicle(string model, string make, string registrationNumber,
            string year, EngineType engine)
        {
            if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentException("Please provide a valid model!");
            }

            //if (string.IsNullOrWhiteSpace(make))
            //{
            //    throw new ArgumentException("Please provide a valid model!");
            //}

            //if (owner == null)
            //{
            //    throw new ArgumentException("Invalid owner!");
            //}

            if (string.IsNullOrWhiteSpace(registrationNumber) || registrationNumber.Length < 6)
            {
                throw new ArgumentException("Invalid registration number. Must be at least 6 characters!");
            }

            if (string.IsNullOrWhiteSpace(year) || year.Any(a => !char.IsDigit(a)) || int.Parse(year) < 1900)
            {
                throw new ArgumentException("Invalid year!");
            }

            this.model = model;
            this.make = make;
            //this.owner = owner;
            this.registrationNumber = registrationNumber;
            this.year = year;
            this.engine = engine;
        }

        public string Model { get => this.model; }

        public string Make { get => this.make; }

        public VehicleType VehicleType { get => this.vehicleType; protected set { this.vehicleType = value; }}

        //public IClient Owner { get => this.owner; }

        public string RegistrationNumber { get => this.registrationNumber;}

        public string Year { get => this.year; }

        public EngineType Engine { get => this.engine; }

        public override string ToString()
        {
            return $"Vehicle: {this.GetType().Name}" + Environment.NewLine +
                   $"-- Make: {this.Make}" + Environment.NewLine +
                   $"-- Model: {this.Model}" + Environment.NewLine +
                   $"-- Year: {this.Year}" + Environment.NewLine +
                   $"-- Engine: {this.Engine}" + Environment.NewLine +
                   $"-- Number of tires: {(int) this.VehicleType}" + Environment.NewLine +
                   $"-- Registration number: {this.RegistrationNumber}" + Environment.NewLine;
            //$"-- Owner: {this.Owner.Name}";
        }
    }
}
