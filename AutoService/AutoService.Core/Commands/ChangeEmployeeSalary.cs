using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService
{
    public class ChangeEmployeeSalary: ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;
        private readonly IEmployeeManager employeeManager;

        public ChangeEmployeeSalary(IDatabase database, IValidateCore coreValidator, IWriter writer, IEmployeeManager employeeManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.employeeManager = employeeManager;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var salary = decimal.Parse(commandParameters[2]);

            this.ChangeSalaryOfEmployee(employee, salary, employeeManager);
        }


        private void ChangeSalaryOfEmployee(IEmployee employee, decimal salary, IEmployeeManager employeeManager)
        {
            employeeManager.SetEmployee(employee);
            employeeManager.ChangeSalary(salary);

            writer.Write( $"Salary of employee {employee.FirstName} {employee.LastName} was successfully set to {salary}");
        }
    }
}