using AutoService.Core.Contracts;
using AutoService.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//showAllEmployeesAtDepartment;ServicesForClients

namespace AutoService.Core.Commands
{
    public class ShowAllEmployeesAtDepartment : ICommand
    {
        private DepartmentType department;
        private readonly IDatabase database;

        public ShowAllEmployeesAtDepartment(IDatabase database)
        {
            this.database = database;
        }
        
        public void ExecuteThisCommand(string[] commandParameters)
        {

            //TODO: uncomment once validation is ready
            //ValidateModel.ExactParameterLength(commandParameters, 2);

            //department = ValidateModel.DepartmentTypeFromString(commandParameters[1], "department");
            department = (DepartmentType)Enum.Parse(typeof(DepartmentType), commandParameters[1]);

            var employeesInDepartment = database.Employees.Where(x => x.Department == department).ToList();
            if (employeesInDepartment.Count == 0)
            {
                throw new ArgumentException($"The are no employees at department: {department}!");
            }

            StringBuilder str = new StringBuilder();

            str.AppendLine($"Employees at: {department} department:");
            var counter = 1;

            foreach (var employee in employeesInDepartment)
            {
                str.AppendLine($"{counter}. {employee.ToString()}");
                counter++;
            }
            Console.WriteLine(str.ToString());

        }
    }
}
