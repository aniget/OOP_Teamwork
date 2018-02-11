using AutoService.Models.Assets.Contracts;
using AutoService.Models.Assets.Events;
using AutoService.Models.Common.Contracts;
using System;

namespace AutoService.Models.Assets
{
    public class BankAccount : Asset, IBankAccount
    {
        public event EventHandler CriticalLimitReached;

        private decimal balance;
        private DateTime registrationDate;
        private decimal criticalLimit;

        public BankAccount(string name, IEmployee responsibleEmployee, string uniqueNumber, DateTime registrationDate) : base(name, responsibleEmployee, uniqueNumber)
        {
            this.Balance = 0;
            this.RegistrationDate = registrationDate;
            this.criticalLimit = 300;
        }

        public decimal Balance
        {
            get => this.balance;
            set
            {
                this.balance = value;
                if (value < 0)
                {
                    throw new ArgumentException("Balance cannot be negative");
                }
                if (this.balance <= this.criticalLimit)
                {
                    CriticalLimitReachedEventArgs args = new CriticalLimitReachedEventArgs
                    {
                        CriticalLimit = criticalLimit
                    };
                    OnCriticalLimitReached(args);
                }
            }
        }

        public DateTime RegistrationDate
        {
            get => this.registrationDate;
            set { this.registrationDate = value; }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                   $"  - Balance: ${this.Balance}";
        }

        public virtual void OnCriticalLimitReached(EventArgs e)
        {
            EventHandler handler = CriticalLimitReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
