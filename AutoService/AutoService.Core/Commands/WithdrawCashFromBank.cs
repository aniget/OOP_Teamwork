using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;

namespace AutoService.Core.Commands
{
    public class WithdrawCashFromBank : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public WithdrawCashFromBank(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 4);

            this.coreValidator.BankAccountsCount(this.database.BankAccounts.Count);
            int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

            var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

            int bankAccountId = this.coreValidator.IntFromString(commandParameters[2], "bankAccountId");

            var bankAccount = this.coreValidator.BankAccountById(this.database.BankAccounts, bankAccountId);

            decimal withdrawAmount = this.coreValidator.DecimalFromString(commandParameters[3], "depositAmount");

            this.WithdrawCashFromBankMethod(bankAccount, withdrawAmount, employee);
        }

        private void WithdrawCashFromBankMethod(BankAccount bankAccount, decimal withdrawAmount, IEmployee employee)
        {
            if (employee.Responsibilities.Contains(ResponsibilityType.Account) || employee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                bankAccount.WithdrawFunds(withdrawAmount);
                this.writer.Write($"{withdrawAmount} BGN were successfully withdrawn by {employee.FirstName} {employee.LastName}");
            }
            else
            {
                throw new ArgumentException($"Employee {employee.FirstName} {employee.LastName} is not allowed to withdraw!");
            }
        }
    }
}
