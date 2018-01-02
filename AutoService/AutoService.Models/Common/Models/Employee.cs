using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;

namespace AutoService.Models.Common.Models
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
                Validate.StringForNullEmpty(value);
                Validate.HasDigitInString(value, "first name");
                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;

            private set
            {
                Validate.StringForNullEmpty(value);
                Validate.HasDigitInString(value, "last name");

                this.lastName = value;
            }
        }

        public decimal Salary
        {
            get => this.salary;

            private set
            {
                Validate.NonNegativeValue(value, "salary");
                
                this.salary = value;
            }
        }
        public string Position
        {
            get => this.position;
            private set
            {
                Validate.StringForNullEmpty(value);
                this.position = value;
            }
        }

        public decimal RatePerMinute
        {
            get => this.ratePerMinute;
            private set
            {
                Validate.NonNegativeValue(value, "rate per minute");

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

        public List<ResponsibilityType> Responsibilities
        {
            get => this.responsibilities;
        }

        public void ChangeSalary(decimal salary)
        {
            this.Salary = salary;
        }

        public void AddResponsibilities(List<ResponsibilityType> value)
        {
            List<ResponsibilityType> resposibilityToBeAdded = new List<ResponsibilityType>();
            List<ResponsibilityType> alreadyHasResponsibilities = new List<ResponsibilityType>();

            foreach (var resp in value)
            {
                if (this.Responsibilities.Any(a => a.Equals(resp)))
                {
                    alreadyHasResponsibilities.Add(resp);
                }
                else
                {
                    this.Responsibilities.Add(resp);
                    resposibilityToBeAdded.Add(resp);
                }
            }

            if (resposibilityToBeAdded.Count > 0)
            {
                Console.WriteLine($"To Employee {this.FirstName} {this.LastName} were successfully added responsibilities {string.Join(", ", resposibilityToBeAdded)}");
            }
            if (alreadyHasResponsibilities.Count > 0)

            {
                Console.WriteLine($"Employee {this.FirstName} {this.LastName} already has these responsibilities: {string.Join(", ", alreadyHasResponsibilities)} ");
            }
        }

        public void RemoveResponsibilities(List<ResponsibilityType> value)
        {
            List<ResponsibilityType> removedResponsibilities = new List<ResponsibilityType>();
            foreach (var responsibility in value)
            {
                if (this.responsibilities.Contains(responsibility))
                {
                    this.responsibilities.Remove(responsibility);
                    removedResponsibilities.Add(responsibility);
                }
                else
                {
                    Console.WriteLine("Employee does not have this responsibility!");
                }
            }
            Console.WriteLine($"Employee {this.FirstName} {this.LastName} were succesfuly declined and removed responsibilities {string.Join(", ", removedResponsibilities)}");
        }

        public void ChangePosition(string position)
        {
            this.Position = position;
        }

        public void FireEmployee()
        {
            if (IsHired)
            {
                this.Responsibilities.Clear();
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
                   string.Format("+++ Responsibilites: {0}", this.Responsibilities.Count == 0 ? "No repsonsibilites" : string.Join(", ", this.Responsibilities));
        }
    }
}
