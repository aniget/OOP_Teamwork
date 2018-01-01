﻿using System;
using AutoService.Core.Validator;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;


namespace AutoService.Models.BusinessProcess.Models22222

{
    public abstract class Order: Work, IOrder
    {
        private readonly ICounterparty supplier;

        protected Order(IEmployee responsibleEmployee, ICounterparty supplier) 
            : base(responsibleEmployee, TypeOfWork.Ordering)
        {
            Validate.CheckNullObject(supplier);
            this.supplier = supplier;
        }

        public ICounterparty Supplier => this.supplier;
    }
}
