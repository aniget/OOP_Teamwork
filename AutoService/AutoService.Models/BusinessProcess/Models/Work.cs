using System;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public abstract class Work : IWork
    {
        public IEmployee ResponsibleEmployee { get; }
        public decimal Price { get; }
        public TypeOfWork Job { get; }
    }
}
