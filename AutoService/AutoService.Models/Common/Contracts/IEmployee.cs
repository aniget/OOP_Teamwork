using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Enums;

namespace AutoService.Models.Contracts
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
        List<ResponsibilityType> Responsibiities { get; }

        void ChangeSalary(decimal salary);
        void ChangePosition(string position);
        void AddResponsibilities(List<ResponsibilityType> value);
        void RemoveResponsibilities(List<ResponsibilityType> value);
        void ChangeRate(decimal ratePerMinute);
        void FireEmployee();
    }
}
