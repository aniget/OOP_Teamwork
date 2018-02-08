using System.Collections.Generic;
using AutoService.Models.Common.Enums;

namespace AutoService.Models.Common.Contracts
{
    public interface IEmployee
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Position { get; set; }
        decimal Salary { get; set; }
        decimal RatePerMinute { get; set; }
        DepartmentType Department { get; set; }
        bool IsHired { get; set; }
        IList<ResponsibilityType> Responsibilities { get; }
    }
}
