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
            this.database = database ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            this.coreValidator.EmployeeCount(this.database.Employees.Count);

            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            this.coreValidator.CheckNullObject(employee);
            
            if (employee.IsHired)
            {
                employee.Responsibilities.Clear();
                employee.IsHired = false;
            }
            else
            {
                throw new ArgumentException("Employee is already fired!");
            }

            this.writer.Write($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }
    }
}
