using System;
using AutoService.Models.Assets.Events;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Assets
{
    public class BankAccount : Asset
    {
        //declaration of the event
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
            protected set
            {
                this.balance = value;
                if (this.balance <= this.criticalLimit)
                {
                    CriticalLimitReachedEventArgs args = new CriticalLimitReachedEventArgs();
                    args.CriticalLimit = criticalLimit;
                    OnCriticalLimitReached(args);
                }
            }
        }

        public DateTime RegistrationDate
        {
            get => this.registrationDate;
            set { this.registrationDate = value; }
        }

        
        public void DepositFunds(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative!");
            }
            this.Balance += amount;
        }

        public void WithdrawFunds(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative!");
            }
            if (this.Balance - amount < 0)
            {
                throw new ArgumentException("Remaining amount cannot be negative!");
            }

            this.Balance -= amount;
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
