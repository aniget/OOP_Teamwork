using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Models.BusinessProcess.Enums
{
    public enum ResponsibilityType
    {
        Manage, //Manager Adds and Removes Responsibilities; only Manager can Rent and Lease for now
        Sell,
        Repair,
        BuyPartForClient,
        BuyPartForWarehouse,
        OrderService, //Order New Telephone/ Internet
        Account,
        WorkInWarehouse,
        Drive //Deliver Parts
            
    }
}
