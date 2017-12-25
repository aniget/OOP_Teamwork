using System;
using System.Dynamic;
using AutoService.Models.BusinessProcess.Enums;

namespace AutoService.Models.Contracts
{
    public interface IWork
    {
        //even when job is performed by external party there is someone from the AutoService who is responsible for the completion
        IEmployee ResponsibleEmployee { get; }

        DateTime StartDateTime { get; }

        bool IsFinished { get; }

        TypeOfWork Job { get; }

        decimal CalculateInternalCost();

    }
}
