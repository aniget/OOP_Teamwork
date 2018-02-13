using AutoService.Core.Contracts;
using AutoService.Models.Common.Enums;
using System;
using System.Linq;
using System.Text;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ShowAllEmployeesAtDepartment : ICommand
    {
        private DepartmentType department;
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public ShowAllEmployeesAtDepartment(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            department = this.coreValidator.DepartmentTypeFromString(commandParameters[1], "department");

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
            this.writer.Write(str.ToString());
        }
    }
}
