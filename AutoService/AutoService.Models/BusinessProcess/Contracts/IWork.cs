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

        DateTime StartDateTime { get; }

        bool IsFinished { get; } // better to leave it here, in order to use the implementation in future classes. Will be true for each order

        TypeOfWork Job { get; }

        decimal CalculateInternalCost(); // any idea for implementation? How to calculate? Maybe predefined internal cost?

    }
}
