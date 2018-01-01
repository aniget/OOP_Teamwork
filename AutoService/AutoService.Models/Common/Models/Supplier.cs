using AutoService.Models.Common.Models;

namespace AutoService.Models.Assets
{
    public class Supplier : CounterParty
    {
        public Supplier(string name, string address, string uniqueNumber) : base(name, address, uniqueNumber)
        {
            
        }
    }
}
