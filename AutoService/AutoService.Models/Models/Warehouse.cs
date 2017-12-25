using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public static class Warehouse
    {
        public static ICollection<IPart> Parts { get; }

        public static void AddPartToWarehouse(IPart part, IEmployee employee)
        {
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse))
            {
                Parts.Add(part);
            }
            else
            {
                throw new ArgumentException("No authorization to buy parts to warehouse.");
            }

        }

        public static void SellPartToClient(IPart part, IEmployee employee, ICar car)
        {
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.Sell))
            {
                Parts.Remove(part);

                //Part sold may be 
            }
            else
            {
                throw new ArgumentException("No authorization to sell parts to clients.");
            }


        }
    }
}
