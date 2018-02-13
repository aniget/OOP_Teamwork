using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class RemoveEmployeeResponsibility : ICommand
    {
        private readonly IWriter writer;
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;

        public RemoveEmployeeResponsibility(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.MinimumParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var responsibilitiesToRemove = commandParameters.Skip(2).ToList();

            List<ResponsibilityType> removedResponsibilities = new List<ResponsibilityType>();

            foreach (var responsibility in responsibilitiesToRemove)
            {
                bool isValid = this.coreValidator.IsValidResponsibilityTypeFromString(responsibility);

                if (isValid)
                {
                    ResponsibilityType enumResponsibility = (ResponsibilityType)Enum.Parse(typeof(ResponsibilityType), responsibility);
                    if (employee.Responsibilities.Contains(enumResponsibility))
                    {
                        employee.Responsibilities.Remove(enumResponsibility);
                        removedResponsibilities.Add(enumResponsibility);
                    }
                    else
                    {
                        writer.Write("Employee does not have this responsibility!");
                    }
                }
            }
            writer.Write($"Employee {employee.FirstName} {employee.LastName} were succesfuly declined and removed responsibilities {string.Join(", ", removedResponsibilities)}");
        }
    }
}
