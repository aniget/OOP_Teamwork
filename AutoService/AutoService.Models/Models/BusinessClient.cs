using System.Collections.Generic;
using System.Net.Http;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public class BusinessClient : Client, IBusiness
    {
        public string Name { get; }
        public string Address { get; }
        public string UniqueNumber { get; }
        public ICollection<IInvoice> Invoices { get; }
        public ICollection<IExternalBusinessJob> ServicesProvided { get; }
    }
}
