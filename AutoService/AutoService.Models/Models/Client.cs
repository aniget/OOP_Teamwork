using System.Collections.Generic;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public abstract class Client : IClient
    {
        //two constructors - one for Individual and one for Company

        public LegalType Legal { get; }
        public string Name { get; }
        public string Address { get; }
        public string UniqueNumber { get; }
        public ICollection<IInvoice> Invoices { get; }
        public decimal Discount { get; }
        public void UpdateDueDays(int dueDays)
        {
            throw new System.NotImplementedException();
        }
    }
}
