using System;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class BankAccount : Asset
    {
        private decimal balance;
        private DateTime registrationDate;

        public BankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate) : base(name, responsibleEmployee)
        {
            this.balance = 0;
            this.registrationDate = DateTime.TryParse(registrationDate.ToString(), out this.registrationDate) ? DateTime.Parse(registrationDate.ToString()) 
                : throw new ArgumentException("Please, enter valid Date in format DD.MM.YYYY!");
        }

        public decimal Balance
        {
            get => this.balance;
            protected set
            {
                this.balance = value;
            }
        }

        public DateTime RegistrationDate
        {
            get => this.registrationDate;
            set { this.registrationDate = value; }
        }

        protected override void ChangeResponsibleEmployee(IEmployee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException("Responsible employee can't be null!");
            }
            if (employee.Responsibiities.Contains(ResponsibilityType.Account) || employee.Responsibiities.Contains(ResponsibilityType.Manage))
            {
                this.ResponsibleEmployee = employee;
            }
            else
            {
                throw new ArgumentException($"Employee cannot be responsible for asset {this.GetType().Name}");
            }
        }
        
        public void DepositFunds(decimal amount)
        {
            if (amount < 0 )
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
    }
}
