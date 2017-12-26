    using System;
using System.Dynamic;
using AutoService.Models.BusinessProcess.Enums;

namespace AutoService.Models.Contracts
{
    public interface IWork
    {
        //even when job is performed by external party there is someone from the AutoService who is responsible for the completion
        IEmployee ResponsibleEmployee { get; }

        decimal Price { get; } //both Sell and Order price here

        DateTime StartDateTime { get; } // to be removed

        bool IsFinished { get; } // to be removed?

        TypeOfWork Job { get; }

        decimal CalculateInternalCost(); // could be made in calculated property?

    }
}
