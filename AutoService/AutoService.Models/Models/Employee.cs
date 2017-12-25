using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public class Employee : IEmployee
    {
        private string firstName;
        private string lastName;
        private decimal salary;
        private string position;
        private decimal ratePerMinute;
        private EmploymentType employmentType;
        private bool isStillHired;

        public Employee(string firstName, string lastName, decimal salary, string position, decimal ratePerMinute, EmploymentType employmentType)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.Position = position;
            this.RatePerMinute = ratePerMinute;
            this.EmploymentType = employmentType;
            this.IsStillHired = true;
        }

        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Invalid first name!");
                }
                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Invalid last name!");
                }
                this.lastName = value;
            }
        }
        public decimal Salary
        {
            get => this.salary;
            
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary cannot be negative!");
                }
                this.salary = value;
            }
        }
        public string Position
        {
            get => this.position;
            private set { this.position = value; }
        }

        public decimal RatePerMinute
        {
            get => this.ratePerMinute;
            private set { this.ratePerMinute = value; } // Add validation
        }

        public EmploymentType EmploymentType
        {
            get { return this.employmentType; }
            set { this.employmentType = value; }
        }

        public DepartmentType Department { get; }

        public bool IsStillHired
        {
            get => this.isStillHired;
            private set { this.isStillHired = value; }
        }

        public ICollection<ResponsibilityType> Responsibiities { get; }

        public void ChangeSalary(decimal salary)
        {
            this.Salary = salary;
        }
        
        public void AddResponsibilities(ICollection<ResponsibilityType> value)
        {
            throw new NotImplementedException();
        }

        public void RemoveResponsibilities(ICollection<ResponsibilityType> value)
        {
            throw new NotImplementedException();
        }

        public void ChangePosition(string position)
        {
            this.Position = position;
        }

        public void FireEmployee()
        {
            this.IsStillHired = false;
        }

        public void ChangeRate(decimal ratePerMinutes)
        {
            this.RatePerMinute = ratePerMinutes;
        }
    }
}
