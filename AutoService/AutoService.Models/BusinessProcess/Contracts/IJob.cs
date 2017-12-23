using System;
using System.Dynamic;

namespace AutoService.Models.Contracts
{
    public interface IJob
    {
        int RequiredTimeInMinutes { get; }

        bool IsFinished { get; }

        decimal CalculateInternalCost();

    }
}
