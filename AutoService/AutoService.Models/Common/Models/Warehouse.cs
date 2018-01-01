using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.Common.Models
{
    public static class Warehouse
    {
        private static List<IStock> stocks;

        static Warehouse()
        {
            stocks = new List<IStock>();
        }

        public static List<IStock> Stocks => stocks;

        public static void AddStockToWarehouse(IStock stock, IEmployee employee)
        {
            Validate.CheckNullObject(stock, employee);
            //only employees with right (Responsibility) to BUY can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibiities.Contains(ResponsibilityType.BuyPartForClient) ||
                employee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse))

                stocks.Add(stock);
            else
            {
                throw new ArgumentException("No authorization to put stock in warehouse parts.");
            }
        }
        
        public static void RemoveStockFromWarehouse(IStock stock, IEmployee employee, IVehicle vehicle)
        {
            Validate.CheckNullObject(stock, employee, vehicle);
            
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage))

                stocks.Remove(stock);
            else
            {
                throw new ArgumentException("No authorization to remove stock from warehouse!");
            }
        }

        public static bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee)
        {
            Validate.CheckNullObject(stockUniqueNumber, employee);
            bool exists = false;
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibiities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibiities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibiities.Contains(ResponsibilityType.WorkInWarehouse))
            {
                if (stocks.Any(x=>x.UniqueNumber == stockUniqueNumber))
                {
                    exists = true;
                }
            }
            else
            {
                throw new ArgumentException("No authorization to remove stock from warehouse!");
            }
            return exists;
        }
    }
}
