using System;
using System.Collections.Generic;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Common.Models
{
    public class Client : CounterParty, IClient
    {
        private int dueDaysAllowed;
        private decimal discount;
        private IList<Vehicle> vehicles;
        private readonly IValidateModel modelValidator;

        public Client(string name, string address, string uniqueNumber, IValidateModel modelValidator) : base(name, address, uniqueNumber)
        {
            this.modelValidator = modelValidator;
            this.dueDaysAllowed = 5;
            this.discount = 0;
            this.vehicles = new List<Vehicle>();
        }

        public IValidateModel ModelValidator { get => this.modelValidator; }
        public int DueDaysAllowed
        {
            get => this.dueDaysAllowed;
            protected set
            {
                this.ModelValidator.NonNegativeValue(value, "due days allowed");

                this.dueDaysAllowed = value;
            }
        }

        public decimal Discount
        {
            get => this.discount;
            protected set
            {
                if (value < 0.00m || value > 1.00m)
                {
                    throw new ArgumentException("Discount cannot be negative or over 100%");
                }
                this.discount = value;
            }
        }

        public IList<Vehicle> Vehicles
        {
            get => this.vehicles;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.ModelValidator.CheckNullObject(vehicle);
            this.vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this.ModelValidator.CheckNullObject(vehicle);
            this.vehicles.Remove(vehicle);
        }

        public void UpdateDueDays(int dueDays)
        {
            this.DueDaysAllowed = dueDays;
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   $"### Due allowed: {this.DueDaysAllowed} day(s)" + Environment.NewLine +
                   $"### Discount: {this.Discount * 100} %";
        }
    }
}