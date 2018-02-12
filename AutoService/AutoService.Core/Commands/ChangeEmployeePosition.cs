using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using System;

namespace AutoService
{
    public class ChangeEmployeePosition : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public ChangeEmployeePosition(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var position = commandParameters[2];

            employee.Position = position;

            writer.Write($"Position of employee {employee.FirstName} {employee.LastName} was successfully set to {position}");
        }
    }
}