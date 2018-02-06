using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System;
using AutoService.Core.Validator;

namespace AutoService.Core.Commandsа
{
    public class HireEmployee : ICommand
    {
        private readonly IDatabase database;

        private readonly IAutoServiceFactory autoServiceFactory;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;

        public HireEmployee(IAutoServiceFactory autoServiceFactory, IDatabase database, IValidateCore coreValidator, IValidateModel modelValidator)
        {
            this.database = database;
            this.autoServiceFactory = autoServiceFactory;
            this.coreValidator = coreValidator;
            this.modelValidator = modelValidator;
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

            ///TODO: make separate validation for models and Core section
            //Validate.EmployeeAlreadyExistOnHire(database, employeeFirstName, employeeLastName, employeeDepartment);

            //To be replaced with AddEmployee method in SERVICES layer
            //this.AddEmployee(employeeFirstName, employeeLastName, position, salary, ratePerMinute, department);
            IEmployee employee = autoServiceFactory.CreateEmployee(employeeFirstName, employeeLastName, position, salary, ratePerMinute, department, modelValidator);

            this.database.Employees.Add(employee);
            Console.WriteLine(employee);
            Console.WriteLine($"Employee {employeeFirstName} {employeeLastName} added successfully with Id {this.database.Employees.Count}");

        }
    }
}
