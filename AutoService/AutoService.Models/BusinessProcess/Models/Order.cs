using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    public class Order: Work, IOrder
    {
        
        public Order(IEmployee responsibleEmployee, decimal price, TypeOfWork job) : base(responsibleEmployee, price, job)
        {
        }

        public ICounterparty Supplier { get; }

    }
}
