using System;
using AutoService.Core.Contracts;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Core.Manager
{
    public class BankAccountManager : IBankAccountManager
    {
        private IBankAccount bankAccount;

        public void SetBankAccount(IBankAccount bankAccount)
        {
            this.bankAccount = bankAccount;
        }

        public void DepositFunds(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative!");
            }
            this.bankAccount.Balance += amount;
        }

        public void WithdrawFunds(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative!");
            }
            if (this.bankAccount.Balance - amount < 0)
            {
                throw new ArgumentException("Remaining amount cannot be negative!");
            }

            this.bankAccount.Balance -= amount;
        }

    }
}