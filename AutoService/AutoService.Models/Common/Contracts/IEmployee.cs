using System.Collections.Generic;
using AutoService.Models.Common.Enums;

namespace AutoService.Models.Common.Contracts
{
    public interface IEmployee
    {
        string FirstName { get; }
        string LastName { get; }
        string Position { get; }
        decimal Salary { get; }
        decimal RatePerMinute { get; }
        DepartmentType Department { get; }
        bool IsHired { get; }
        List<ResponsibilityType> Responsibilities { get; }

        void ChangeSalary(decimal salary);
        void ChangePosition(string position);
        void AddResponsibilities(List<ResponsibilityType> value);
        void RemoveResponsibilities(List<ResponsibilityType> value);
        void ChangeRate(decimal ratePerMinute);
        void FireEmployee();
    }
}
