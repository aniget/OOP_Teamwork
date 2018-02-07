using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class FireEmployee : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public FireEmployee(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            this.coreValidator.EmployeeCount(this.database.Employees.Count);

            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            this.Fire(employee);

        }
        private void Fire(IEmployee employee)
        {
            this.coreValidator.CheckNullObject(employee);
            employee.FireEmployee();

            this.writer.Write($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }
    }
}
