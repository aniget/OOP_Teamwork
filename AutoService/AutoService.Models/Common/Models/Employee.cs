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
        private IList<ResponsibilityType> responsibilities;
        private readonly IValidateModel modelValidator;

        public Employee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department, IValidateModel modelValidator)
        {
            this.modelValidator = modelValidator;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Position = position;
            this.Salary = salary;
            this.RatePerMinute = ratePerMinute;
            this.Department = department;
            this.isHired = true;
            this.responsibilities = new List<ResponsibilityType>();
        }

        public IValidateModel ModelValidator
        {
            get => this.modelValidator;
        }

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.ModelValidator.StringForNullEmpty(value);
                this.ModelValidator.HasDigitInString(value, "first name");
                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;

             set
            {
                this.ModelValidator.StringForNullEmpty(value);
                this.ModelValidator.HasDigitInString(value, "last name");

                this.lastName = value;
            }
        }

        public decimal Salary
        {
            get => this.salary;

            set
            {
                this.ModelValidator.NonNegativeValue(value, "salary");

                this.salary = value;
            }
        }
        public string Position
        {
            get => this.position;
            set
            {
                this.ModelValidator.StringForNullEmpty(value);
                this.position = value;
            }
        }

        public decimal RatePerMinute
        {
            get => this.ratePerMinute;
            set
            {
                this.ModelValidator.NonNegativeValue(value, "rate per minute");

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
            set
            {
                if (this.isHired)
                {
                    this.isHired = false;
                }
                else
                {
                    throw new ArgumentException("Employee is already fired!");
                }
            }
        }

        public IList<ResponsibilityType> Responsibilities
        {
            get => this.responsibilities;
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
