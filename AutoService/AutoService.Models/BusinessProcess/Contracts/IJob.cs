using System;

namespace AutoService.Models.Contracts
{
    public interface IJob
    {
        int RequiredTimeInMinutes { get; }

        decimal CalculateInternalCost();

    }
}
