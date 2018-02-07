using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets;

namespace AutoService.Core.Commands
{
    public class DepositCashInBank : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public DepositCashInBank(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            this.coreValidator.BankAccountsCount(this.database.BankAccounts.Count);

            int bankAccountId = this.coreValidator.IntFromString(commandParameters[1], "bankAccountId");

            BankAccount bankAccount = this.coreValidator.BankAccountById(this.database.BankAccounts, bankAccountId);

            decimal depositAmount = this.coreValidator.DecimalFromString(commandParameters[2], "depositAmount");

            this.DepositCashInBankAccount(bankAccount, depositAmount);
        }

        private void DepositCashInBankAccount(BankAccount bankAccount, decimal depositAmount)
        {
            bankAccount.DepositFunds(depositAmount);
            this.writer.Write($"{depositAmount} BGN were successfully added to bank account {bankAccount.Name}");
        }

    }
}
