using System;

namespace AutoService.Models.Assets.Contracts
{
    public interface IBankAccount : IAsset
    {
        event EventHandler CriticalLimitReached;

        decimal Balance { get; set; }

        DateTime RegistrationDate { get; set; }

        void OnCriticalLimitReached(EventArgs e);
    }
}