using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IEmployee
    {
        string FirstName { get; }
        string LastName { get; }
        decimal Salary { get; }
        string Position { get; }
        EmploymentType EmploymentType { get; }
        bool IsStillHired { get; }

        void ChangeSalary(decimal salary);
        void ChangePosition(string position);
        void FireEmployee();
    }
}
