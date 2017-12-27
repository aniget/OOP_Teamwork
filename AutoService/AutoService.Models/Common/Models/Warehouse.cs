using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Models
{
    public static class Warehouse
    {
        public static ICollection<IStock> Stocks { get; }

        public static void AddPartToWarehouse(IStock stock, IEmployee employee)
        {
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse))
            {
                Stocks.Add(stock);
            }
            else
            {
                throw new ArgumentException("No authorization to buy parts to warehouse.");
            }

        }

        public static void SellPartToClient(IStock stock, IEmployee employee, Vehicle car)
        {
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.Sell))
            {
                Stocks.Remove(stock);

                //Part sold may be 
            }
            else
            {
                throw new ArgumentException("No authorization to sell parts to clients.");
            }


        }
    }
}
