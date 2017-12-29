using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public class Employee : IEmployee
    {
        private string firstName;
        private string lastName;
        private string position;
        private decimal salary;
        private decimal ratePerMinute;
        private DepartmentType department;
        private bool isHired;
        private List<ResponsibilityType> responsibilities;

        public Employee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
            this.Salary = salary;
            this.RatePerMinute = ratePerMinute;
            this.Department = department;
            this.isHired = true;
            this.responsibilities = new List<ResponsibilityType>();
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
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid position!");
                }
                this.position = value;
            }
        }

        public decimal RatePerMinute
        {
            get => this.ratePerMinute;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid rate!");
                }
                this.ratePerMinute = value;
            }
        }

        public DepartmentType Department
        {
            get => this.department;
            set { this.department = value; }
        }

        public bool IsHired
        {
            get => this.isHired;
            private set { this.isHired = value; }
        }

        public List<ResponsibilityType> Responsibiities
        {
            get => this.responsibilities;
        }

        public void ChangeSalary(decimal salary)
        {
            this.Salary = salary;
        }

        public void AddResponsibilities(List<ResponsibilityType> value)
        {
            List<ResponsibilityType> result = new List<ResponsibilityType>();
            List<ResponsibilityType> hasResp = new List<ResponsibilityType>();

            foreach (var resp in value)
            {
                if (!responsibilities.Contains(resp))
                {
                    this.responsibilities.Add(resp);
                    result.Add(resp);
                }
                if (responsibilities.Contains(resp))
                {
                    hasResp.Add(resp);
                }

            }
            if (result.Count > 0)
            {
                Console.WriteLine($"To Employee {this.FirstName} {this.LastName} were succesfuly added responsibilities {string.Join(", ", result)}");
            }
            if (hasResp.Count > 0)

            {
                Console.WriteLine($"Employee {this.FirstName} {this.LastName} already has this responsibilities: {string.Join(", ", hasResp)} ");
            }

        }

        public void RemoveResponsibilities(List<ResponsibilityType> value)
        {
            foreach (var responsibility in value)
            {
                if (this.responsibilities.Contains(responsibility))
                {
                    this.responsibilities.Remove(responsibility);
                }
                else
                {
                    throw new ArgumentException("Employee does not have this responsibility!");
                }
            }
        }

        public void ChangePosition(string position)
        {
            this.Position = position;
        }

        public void FireEmployee()
        {
            if (IsHired)
            {
                this.IsHired = false;
            }
            else
            {
                throw new ArgumentException("Employee is already fired!");
            }
        }

        public void ChangeRate(decimal ratePerMinutes)
        {
            this.RatePerMinute = ratePerMinutes;
        }

        public override string ToString()
        {
            return $"Employee names: {this.FirstName} {this.LastName}" + Environment.NewLine +
                   $"+++ Position: {this.Position}" + Environment.NewLine +
                   $"+++ Rate per minute: {this.RatePerMinute} $" + Environment.NewLine +
                   $"+++ Department: {this.Department}" + Environment.NewLine +
                   string.Format("+++ Responsibilites: {0}", this.Responsibiities.Count == 0 ? "No repsonsibilites" : string.Join(", ", this.Responsibiities));
        }
    }
}
