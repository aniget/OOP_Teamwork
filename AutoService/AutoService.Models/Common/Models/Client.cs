using System;
using AutoService.Models.Common.Models;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Client : CounterParty, IClient
    {
        private int dueDaysAllowed;
        private decimal discount;

        public Client(string name, string address, string uniqueNumber) : base(name, address, uniqueNumber)
        {
            this.dueDaysAllowed = 5;
            this.discount = 0;
        }

        public int DueDaysAllowed
        {
            get => this.dueDaysAllowed;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Days cannot be negative value!");
                }
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