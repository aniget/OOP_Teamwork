using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Models.BusinessProcess.Models22222

{
    public abstract class Order: Work, IOrder
    {
        private readonly ICounterparty supplier;
        

        protected Order(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty supplier) 
            : base(responsibleEmployee, TypeOfWork.Ordering)
        {
            this.supplier = supplier ?? throw new ArgumentException("Supplier cannot be null");
            //TODO: check if order is made by nonexistent supplier
        
        }

        public ICounterparty Supplier => this.supplier;
    }
}
