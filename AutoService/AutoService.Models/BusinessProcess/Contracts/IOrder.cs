﻿using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrder: IWork
    {
        ISupplier Supplier { get; }

        //PaymentType Payment { get; } // let's remove to keep simple

        //CreditTerm Credit { get; } // let's remove to keep simple
    }
}
