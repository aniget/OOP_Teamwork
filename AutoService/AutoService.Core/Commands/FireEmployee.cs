using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;

namespace AutoService.Core.Commands
{
    public class FireEmployee : ICommand
    {
        private readonly IDatabase database;

        private readonly IValidateCore coreValidator;

        public FireEmployee(IDatabase database, IValidateCore coreValidator)
        {
            this.database = database;
            this.coreValidator = coreValidator;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            this.coreValidator.EmployeeCount(this.database.Employees.Count);

            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            this.FireEmployeeMethod(employee);

        }
        private void FireEmployeeMethod(IEmployee employee)
        {
            this.coreValidator.CheckNullObject(employee);
            employee.FireEmployee();

            Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }
    }
}
