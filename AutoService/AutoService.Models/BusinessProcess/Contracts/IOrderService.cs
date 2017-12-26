using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IOrderService : IOrder
    {
        WWW_IOrderedService Service { get; }

        bool ServiceIsDelivered { get; }

    }
}
