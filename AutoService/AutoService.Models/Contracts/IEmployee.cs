using System.Collections;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Enums;
using AutoService.Models.Models;

namespace AutoService.Models.Contracts
{
    public interface IEmployee
    {
        string FirstName { get; }
        string LastName { get; }
        decimal Salary { get; }
        decimal RatePerMinute { get; }
        EmploymentType EmploymentType { get; }
        DepartmentType Department { get; }
        bool IsStillHired { get; }
        ICollection<ResponsibilityType> Responsibiities { get; }

        void ChangeSalary(decimal salary);
        void ChangePosition(string position);
        void AddResponsibilities(ICollection<ResponsibilityType> value);
        void RemoveResponsibilities(ICollection<ResponsibilityType> value);
        void ChangeRate(decimal ratePerMinute);

        void FireEmployee();
    }
}
