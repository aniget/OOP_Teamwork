using System;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public abstract class Work : IWork
    {
        public Contracts.IEmployee ResponsibleEmployee { get; }
        public decimal Price { get; }
        public DateTime StartDateTime { get; }
        public bool IsFinished { get; }
        public TypeOfWork Job { get; }

        public decimal CalculateInternalCost()
        {
            throw new System.NotImplementedException();
        }
    }
}
