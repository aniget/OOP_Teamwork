using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Common.Models
{
    public class Supplier : CounterParty, ICounterparty
    {
        private readonly bool interfaceIsAvailable;

        public Supplier(string name, string address, string uniqueNumber, bool interfaceIsAvailable) : base(name, address, uniqueNumber)
        {
            this.interfaceIsAvailable = interfaceIsAvailable;
        }

        public bool InterfaceIsAvailable => this.interfaceIsAvailable;

    }
}
