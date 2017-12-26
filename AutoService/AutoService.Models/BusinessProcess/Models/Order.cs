using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    public class Order: Work, IOrder
    {
        public ISupplier Supplier { get; }
        public decimal Price { get; }
        public PaymentType Payment { get; }
        public CreditTerm Credit { get; }
    }
}
