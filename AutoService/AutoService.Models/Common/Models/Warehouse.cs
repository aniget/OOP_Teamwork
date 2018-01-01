using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Models.Common.Models
{
    public class Warehouse
    {
        private List<IStock> availableStocks;

        public Warehouse()
        {
            this.availableStocks = new List<IStock>();
        }

        public List<IStock> AvailableStocks => this.availableStocks;

        public void AddStockToWarehouse(IStock stock, IEmployee employee)
        {
            Validate.CheckNullObject(stock, employee);
            //only employees with right (Responsibility) to BUY can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibilities.Contains(ResponsibilityType.BuyPartForClient) ||
                employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))

                availableStocks.Add(stock);
            else
            {
                throw new ArgumentException("No authorization to put stock in warehouse parts.");
            }
        }
        
        public void RemoveStockFromWarehouse(IStock stock, IEmployee employee)
        {
            Validate.CheckNullObject(stock, employee);
            
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage))

                availableStocks.Remove(stock);
            else
            {
                throw new ArgumentException("No authorization to remove stock from warehouse!");
            }
        }

        public bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee)
        {
            Validate.CheckNullObject(stockUniqueNumber, employee);
            bool exists = false;
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))
            {
                if (availableStocks.Any(x=>x.UniqueNumber == stockUniqueNumber))
                {
                    exists = true;
                }
            }
            else
            {
                throw new ArgumentException("No authorization to check stock in warehouse!");
            }
            return exists;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var counter = 1;

            foreach (var stock in this.AvailableStocks.OrderBy(ob => ob.Supplier.Name))
            {
                sb.AppendLine(counter + ". " + stock + Environment.NewLine);
                counter++;
            }

            return sb.ToString();
        }
    }
}
