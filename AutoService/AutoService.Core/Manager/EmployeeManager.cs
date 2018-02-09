using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;

namespace AutoService.Core.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployee employee;
        private readonly IWriter writer;

        public EmployeeManager() { }

        public EmployeeManager(IWriter writer)
        {
            this.writer = writer;
        }

        public void SetEmployee(IEmployee employee)
        {
            this.employee = employee ?? throw new ArgumentException("Null employee provided!");
        }

        public void ChangeSalary(decimal salary)
        {

            this.employee.Salary = salary;
        }

        public void AddResponsibilities(IList<ResponsibilityType> responsibilities)
        {
            List<ResponsibilityType> resposibilityToBeAdded = new List<ResponsibilityType>();
            List<ResponsibilityType> alreadyHasResponsibilities = new List<ResponsibilityType>();

            foreach (var resp in responsibilities)
            {
                if (this.employee.Responsibilities.Any(a => a.Equals(resp)))
                {
                    alreadyHasResponsibilities.Add(resp);
                }
                else
                {
                    this.employee.Responsibilities.Add(resp);
                    resposibilityToBeAdded.Add(resp);
                }
            }

            if (resposibilityToBeAdded.Count > 0)
            {
                writer.Write($"To Employee {this.employee.FirstName} {this.employee.LastName} were successfully added responsibilities {string.Join(", ", resposibilityToBeAdded)}");
            }

            if (alreadyHasResponsibilities.Count > 0)
            {
                writer.Write($"Employee {this.employee.FirstName} {this.employee.LastName} already has these responsibilities: {string.Join(", ", alreadyHasResponsibilities)}");
            }
        }

        public void RemoveResponsibilities(IList<ResponsibilityType> responsibilities)
        {
            List<ResponsibilityType> removedResponsibilities = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilities)
            {
                if (this.employee.Responsibilities.Contains(responsibility))
                {
                    this.employee.Responsibilities.Remove(responsibility);
                    removedResponsibilities.Add(responsibility);
                }
                else
                {
                    writer.Write("Employee does not have this responsibility!");
                }
            }
            writer.Write($"Employee {this.employee.FirstName} {this.employee.LastName} were succesfuly declined and removed responsibilities {string.Join(", ", removedResponsibilities)}");
        }

        public void ChangePosition(string position)
        {
            this.employee.Position = position;
        }

        public void FireEmployee()
        {
            if (this.employee.IsHired)
            {
                this.employee.Responsibilities.Clear();
                this.employee.IsHired = false;
            }
            else
            {
                throw new ArgumentException("Employee is already fired!");
            }
        }

        public void ChangeRate(decimal ratePerMinutes)
        {
            this.employee.RatePerMinute = ratePerMinutes;
        }
    }
}