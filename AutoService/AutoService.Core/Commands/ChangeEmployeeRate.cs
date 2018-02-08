using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;

namespace AutoService.Core.Commands
{
    public class ChangeEmployeeRate : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;

        public ChangeEmployeeRate(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {

            coreValidator.ExactParameterLength(commandParameters, 3);

            this.coreValidator.EmployeeCount(database.Employees.Count);

            var employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(database.Employees, employeeId);

            var ratePerMinute = this.coreValidator.DecimalFromString(commandParameters[2], "ratePerMinute");
            
            this.ChangeRateOfEmployee(employee, ratePerMinute);

        }

        private void ChangeRateOfEmployee(IEmployee employee, decimal ratePerMinute)
        {
            employee.ChangeRate(ratePerMinute);
            writer.Write($"Rate per minute of employee {employee.FirstName} {employee.LastName} was successfully set to {ratePerMinute} $");
        }
    }
}
