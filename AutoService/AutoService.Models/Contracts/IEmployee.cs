using AutoService.Models.Enums;
using AutoService.Models.Models;

namespace AutoService.Models.Contracts
{
    public interface IEmployee
    {
        string FirstName { get; }
        string LastName { get; }
        decimal Salary { get; }
        Position Position { get; }
        EmploymentType EmploymentType { get; }
        bool IsStillHired { get; }

        void ChangeSalary(decimal salary);
        void ChangePosition(Position position);
        void FireEmployee();
    }
}
