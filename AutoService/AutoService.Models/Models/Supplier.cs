using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    class Supplier :ISupplier
    {
        public string Name { get; }
        public string Address { get; }
        public string UniqueNumber { get; }
        public ICollection<IInvoice> Invoices { get; }
    }
}
