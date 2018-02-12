using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Core.Validator;
using System;

namespace AutoService.Core.Commandsа
{
    public class HireEmployee : ICommand
    {
        //hireEmployee;Jo;Ma;CleanerManager;1000;20;Management
        
        private readonly IDatabase database;
        private readonly IAutoServiceFactory autoServiceFactory;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;

        public HireEmployee(IAutoServiceFactory autoServiceFactory, IDatabase database, IValidateCore coreValidator, IWriter writer, IValidateModel modelValidator)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.autoServiceFactory = autoServiceFactory ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.modelValidator = modelValidator ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 7);

            var employeeFirstName = commandParameters[1];
            var employeeLastName = commandParameters[2];
            var position = commandParameters[3];
            var salary = this.coreValidator.DecimalFromString(commandParameters[4], "salary");
            var ratePerMinute = this.coreValidator.DecimalFromString(commandParameters[5], "ratePerMinute");
            var employeeDepartment = commandParameters[6];
            var department = this.coreValidator.DepartmentTypeFromString(employeeDepartment, "department");

            coreValidator.EmployeeAlreadyExistOnHire(database, employeeFirstName, employeeLastName, employeeDepartment);

            var employee = autoServiceFactory.CreateEmployee(employeeFirstName, employeeLastName, position, salary, ratePerMinute, department, modelValidator);

            this.database.Employees.Add(employee);

            writer.Write(employee.ToString());
            writer.Write($"Employee {employeeFirstName} {employeeLastName} added successfully with Id {this.database.Employees.Count}");
        }
    }
}
