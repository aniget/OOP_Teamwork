using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Core.Commands
{
    public class RemoveEmployeeResponsibility : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;

        public RemoveEmployeeResponsibility(IDatabase database, IValidateCore coreValidator)
        {
            this.database = database;
            this.coreValidator = coreValidator;
        }


        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.MinimumParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();

            this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

        }


        private void RemoveResponsibilitiesToEmployee(IEmployee employee, string[] responsibilitiesToRemove)
        {
            var responsibilitesToRemove = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilitiesToRemove)
            {
                bool isValid = this.coreValidator.IsValidResponsibilityTypeFromString(responsibility);

                if (isValid)
                {
                    ResponsibilityType currentResponsibility;
                    Enum.TryParse(responsibility, out currentResponsibility);
                    responsibilitesToRemove.Add(currentResponsibility);
                }

            }
            employee.RemoveResponsibilities(responsibilitesToRemove);
        }


    }
}
