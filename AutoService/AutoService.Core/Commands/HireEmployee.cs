using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System;

namespace AutoService.Core.Commands
{
    public class HireEmployee : ICommand
    {
        private readonly IDatabase database;

        //private readonly IList<IEmployee> employees;

        private readonly IAutoServiceFactory autoServiceFactory;

        public HireEmployee(IAutoServiceFactory autoServiceFactory, IDatabase database)
        {
            this.database = database;
            this.autoServiceFactory = autoServiceFactory;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            ValidateModel.ExactParameterLength(commandParameters, 7);

            var employeeFirstName = commandParameters[1];
            var employeeLastName = commandParameters[2];
            var position = commandParameters[3];

            var salary = ValidateModel.DecimalFromString(commandParameters[4], "salary");

            var ratePerMinute = ValidateModel.DecimalFromString(commandParameters[5], "ratePerMinute");

            var employeeDepartment = commandParameters[6];
            var department = ValidateModel.DepartmentTypeFromString(employeeDepartment, "department");

            ///TODO: make separate validation for models and Core section
            //Validate.EmployeeAlreadyExistOnHire(database, employeeFirstName, employeeLastName, employeeDepartment);

            //To be replaced with AddEmployee method in SERVICES layer
            //this.AddEmployee(employeeFirstName, employeeLastName, position, salary, ratePerMinute, department);
            IEmployee employee = autoServiceFactory.CreateEmployee(employeeFirstName, employeeLastName, position, salary, ratePerMinute, department);

            this.database.Employees.Add(employee);
            Console.WriteLine(employee);
            Console.WriteLine($"Employee {employeeFirstName} {employeeLastName} added successfully with Id {this.database.Employees.Count}");

        }
    }
}
