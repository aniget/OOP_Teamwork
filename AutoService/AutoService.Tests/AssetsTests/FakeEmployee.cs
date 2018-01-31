using System.Collections.Generic;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;

namespace AutoService.Tests.AssetsTests
{
    internal class FakeEmployee : IEmployee
    {
        public string FirstName => throw new System.NotImplementedException();

        public string LastName => throw new System.NotImplementedException();

        public string Position => throw new System.NotImplementedException();

        public decimal Salary => throw new System.NotImplementedException();

        public decimal RatePerMinute => throw new System.NotImplementedException();

        public DepartmentType Department => throw new System.NotImplementedException();

        public bool IsHired => throw new System.NotImplementedException();

        public List<ResponsibilityType> Responsibilities => throw new System.NotImplementedException();

        public void AddResponsibilities(List<ResponsibilityType> value)
        {
            throw new System.NotImplementedException();
        }

        public void ChangePosition(string position)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeRate(decimal ratePerMinute)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeSalary(decimal salary)
        {
            throw new System.NotImplementedException();
        }

        public void FireEmployee()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveResponsibilities(List<ResponsibilityType> value)
        {
            throw new System.NotImplementedException();
        }
    }
}