using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public class Vendor : Company, IVendor
    {
        public Vendor(string name, string address, string uniqueNumber, decimal creditLimit) : base(name, address, uniqueNumber, creditLimit)
        {
        }

        public Vendor(string name, string address, string uniqueNumber, decimal creditLimit, string taxNumber) : base(name, address, uniqueNumber, creditLimit, taxNumber)
        {
        }

        public void PayInvoices()
        {
            throw new System.NotImplementedException();
        }
    }
}
