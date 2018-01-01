using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class OrderStock : Order, IOrderStock
    {
        private readonly IStock stock;

        public OrderStock(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock) 
            : base(responsibleEmployee, supplier)
        {
            Validate.CheckNullObject(stock);
            this.stock = stock;
        }

        public IStock Stock
        {
            get => this.stock;
        }
    }
}
