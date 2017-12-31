using System;
using System.Collections.Generic;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Common.Models
{
    public static class Warehouse
    {


        public static List<IStock> Stocks;

        static Warehouse()
        {
            Stocks = new List<IStock>();
        }
        public static void AddStockToWarehouse(IStock stock, IEmployee employee)
        {
            //only employees with right (Responsibility) to BUY can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibiities.Contains(ResponsibilityType.BuyPartForClient) ||
                employee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse))

                Stocks.Add(stock);
            else
            {
                throw new ArgumentException("No authorization to buy parts.");
            }
        }
        
        public static void RemoveStockFromWarehouse(IStock stock, IEmployee employee, IVehicle vehicle)
        {
            if (stock == null) throw new ArgumentNullException(nameof(stock));
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
            
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage))

                Stocks.Remove(stock);
            else
            {
                throw new ArgumentException("No authorization to sell parts to clients.");
            }
        }
    }
}
