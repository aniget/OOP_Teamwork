using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public abstract class Job : IJob
    {
        public int RequiredTimeInMinutes { get; }

        public bool IsFinished { get; }

        public decimal CalculateInternalCost()
        {
            throw new System.NotImplementedException();
        }
    }
}
