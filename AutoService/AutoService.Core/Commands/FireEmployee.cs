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
        private readonly IEmployeeManager employeeManager;

        public FireEmployee(IDatabase database, IValidateCore coreValidator, IWriter writer, IEmployeeManager employeeManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.employeeManager = employeeManager;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            this.coreValidator.EmployeeCount(this.database.Employees.Count);

            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            this.Fire(employee, employeeManager);

        }
        private void Fire(IEmployee employee, IEmployeeManager employeeManager)
        {
            this.coreValidator.CheckNullObject(employee, employeeManager);
            employeeManager.SetEmployee(employee);
            employeeManager.FireEmployee();
            this.writer.Write($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }
    }
}
