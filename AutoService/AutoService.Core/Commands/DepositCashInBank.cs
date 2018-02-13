using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Core.Commands
{
    public class DepositCashInBank : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public DepositCashInBank(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor <IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            this.coreValidator.BankAccountsCount(this.database.BankAccounts.Count);

            int bankAccountId = this.coreValidator.IntFromString(commandParameters[1], "bankAccountId");

            IBankAccount bankAccount = this.coreValidator.BankAccountById(this.database.BankAccounts, bankAccountId);

            decimal depositAmount = this.coreValidator.DecimalFromString(commandParameters[2], "depositAmount");

            if (depositAmount < 0)
            {
                throw new ArgumentException("Amount cannot be negative!");
            }
            bankAccount.Balance += depositAmount;

            writer.Write($"{depositAmount} BGN were successfully added to bank account {bankAccount.Name}");
        }

    }
}
