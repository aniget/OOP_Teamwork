using System.Collections.Generic;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public abstract class Client : IClient
    {
        public string Name { get; }
        public string Address { get; }
        public string UniqueNumber { get; }
        public ICollection<IInvoice> Invoices { get; }
        public int DueDaysAllowed { get; }
        public decimal Discount { get; }
        public void UpdateDueDays(int dueDays)
        {
            throw new System.NotImplementedException();
        }
    }
}
