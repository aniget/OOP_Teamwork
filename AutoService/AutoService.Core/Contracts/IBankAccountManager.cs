using AutoService.Models.Assets.Contracts;

namespace AutoService.Core.Contracts
{
    public interface IBankAccountManager
    {
        void SetBankAccount(IBankAccount bankAccount);

        void DepositFunds(decimal amount);

        void WithdrawFunds(decimal amount);
    }
}
