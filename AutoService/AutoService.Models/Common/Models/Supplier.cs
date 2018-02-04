using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Common.Models
{
    public class Supplier : CounterParty, ICounterparty
    {
        public Supplier(string name, string address, string uniqueNumber) : base(name, address, uniqueNumber)
        {
            
        }
    }
}
