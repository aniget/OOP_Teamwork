using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System;

namespace AutoService.Core.Commands
{
    public class HireEmployeeCommand : ICommand
    {
        private readonly IDatabase database;

        //private readonly IList<IEmployee> employees;

        private readonly IAutoServiceFactory autoServiceFactory;

        public HireEmployeeCommand(IAutoServiceFactory autoServiceFactory, IDatabase database)
        {
            this.database = database;
            this.autoServiceFactory = autoServiceFactory;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            Validate.ExactParameterLength(commandParameters, 7);

            var employeeFirstName = commandParameters[1];
            var employeeLastName = commandParameters[2];
            var position = commandParameters[3];

            var salary = Validate.DecimalFromString(commandParameters[4], "salary");

            var ratePerMinute = Validate.DecimalFromString(commandParameters[5], "ratePerMinute");

            var employeeDepartment = commandParameters[6];
            var department = Validate.DepartmentTypeFromString(employeeDepartment, "department");

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
