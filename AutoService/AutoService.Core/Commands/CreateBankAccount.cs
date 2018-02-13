using AutoService.Core.Contracts;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using System;

namespace AutoService.Core.Commands
{
    public class CreateBankAccount : ICommand
    {
        private readonly IDatabase database;
        private readonly IAutoServiceFactory factory;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public CreateBankAccount(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.factory = processorLocator.GetProcessor<IAutoServiceFactory>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 4);

            if (this.database.Employees.Count == 0)
            {
                throw new InvalidOperationException(
                    "No employees currently in the service! You need to hire one then open the bank account :)");
            }

            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            string assetName = commandParameters[2];

            this.coreValidator.ValidateBankAccount(commandParameters[3]);
            string bankAccountNumber = commandParameters[3];

            DateTime currentAssetDate = this.database.LastAssetDate.AddDays(5); //fixed date in order to check zero tests

            this.Create(employee, assetName, currentAssetDate, bankAccountNumber);
            this.database.LastAssetDate = currentAssetDate;
        }

        private void Create(IEmployee employee, string assetName, DateTime currentAssetDate,
            string uniqueNumber)
        {
            if (employee.Responsibilities.Contains(ResponsibilityType.Account) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                IBankAccount bankAccountToAdd =
                    this.factory.CreateBankAccount(assetName, employee, uniqueNumber, currentAssetDate);
                bankAccountToAdd.CriticalLimitReached += CriticalAmountReached;
                this.database.BankAccounts.Add(bankAccountToAdd);
                this.writer.Write(
                     $"Asset {assetName} was created successfully by his responsible employee {employee.FirstName} {employee.LastName}");
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {employee.FirstName} {employee.LastName} does not have the required repsonsibilities to register asset {assetName}");
            }
        }


        private void CriticalAmountReached(object sender, EventArgs e)
        {
            this.writer.Write("The minimum threshold of 300 BGN was reached! Please deposit some funds!");

        }
    }
}
