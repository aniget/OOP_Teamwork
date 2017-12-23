using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public abstract class Client : IClient
    {
        public int DueDaysAllowed { get; }
        public void UpdateDueDays(int dueDays)
        {
            throw new System.NotImplementedException();
        }
    }
}
