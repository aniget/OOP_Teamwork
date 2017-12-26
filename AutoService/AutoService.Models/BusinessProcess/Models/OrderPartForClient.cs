using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    class OrderPartForClient: OrderPart, IOrderPartForClient
    {
        public void BuyPartForClientCar(ICar car)
        {
            throw new NotImplementedException();
        }
    }
}
