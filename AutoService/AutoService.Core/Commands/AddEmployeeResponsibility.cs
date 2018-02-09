using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class AddEmployeeResponsibility : ICommand
    {
        private readonly IWriter writer;
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;

        public AddEmployeeResponsibility(IWriter writer, IDatabase database, IValidateCore coreValidator)
        {
            this.writer = writer;
            this.database = database;
            this.coreValidator = coreValidator;
        }


        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.MinimumParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var responsibilitiesToAdd = commandParameters.Skip(2).ToList();

            List<ResponsibilityType> resposibilitiesToBeAdded = new List<ResponsibilityType>();
            List<ResponsibilityType> alreadyHasResponsibilities = new List<ResponsibilityType>();

            foreach (var responsibility in responsibilitiesToAdd)
            {
                bool isValid = this.coreValidator.IsValidResponsibilityTypeFromString(responsibility);

                if (isValid)
                {
                    var enumResponsibility = (ResponsibilityType)Enum.Parse(typeof(ResponsibilityType), responsibility);
                    if (employee.Responsibilities.Any(a => a.Equals(enumResponsibility)))
                    {
                        alreadyHasResponsibilities.Add(enumResponsibility);
                    }
                    else
                    {
                        employee.Responsibilities.Add(enumResponsibility);
                        resposibilitiesToBeAdded.Add(enumResponsibility);
                    }
                }
            }

            if (resposibilitiesToBeAdded.Count > 0)
                writer.Write($"To Employee {employee.FirstName} {employee.LastName} were successfully added responsibilities {string.Join(", ", resposibilitiesToBeAdded)}");

            if (alreadyHasResponsibilities.Count > 0)
                writer.Write($"Employee {employee.FirstName} {employee.LastName} already has these responsibilities: {string.Join(", ", alreadyHasResponsibilities)}");
        }


    }
}