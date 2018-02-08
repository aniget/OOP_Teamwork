using System.Collections.Generic;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;

namespace AutoService.Core.Contracts
{
    public interface IEmployeeManager
    {
        void SetEmployee(IEmployee employee);

        void ChangeSalary(decimal salary);

        void AddResponsibilities(IList<ResponsibilityType> value);

        void RemoveResponsibilities(IList<ResponsibilityType> value);

        void ChangePosition(string position);

        void FireEmployee();

        void ChangeRate(decimal ratePerMinutes);
    }
}