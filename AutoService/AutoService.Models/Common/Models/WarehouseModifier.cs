using System;
using System.Linq;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;

namespace AutoService.Models.Common.Models
{
    public class WarehouseModifier
    {
        private IWarehouse warehouse;
        public WarehouseModifier(IWarehouse warehouse)
        {
            this.warehouse = warehouse;
        }

        public void AddStockToWarehouse(IStock stock, IEmployee employee)
        {
            ValidateModel.CheckNullObject(stock, employee);
            //only employees with right (Responsibility) to BUY can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibilities.Contains(ResponsibilityType.BuyPartForClient) ||
                employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))
            {
                this.warehouse.AvailableStocks.Add(stock);
            }
            else
            {
                throw new ArgumentException("No authorization to put stock in warehouse parts.");
            }
        }

        public void RemoveStockFromWarehouse(IStock stock, IEmployee employee)
        {
            ValidateModel.CheckNullObject(stock, employee);

            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage))

                this.warehouse.AvailableStocks.Remove(stock);
            else
            {
                throw new ArgumentException("No authorization to remove stock from warehouse!");
            }
        }

        public bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee)
        {
            ValidateModel.CheckNullObject(stockUniqueNumber, employee);
            bool exists = false;
            //only employees with right (Responsibility) to SELL can perform this work
            if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
                employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))
            {
                if (this.warehouse.AvailableStocks.Any(x => x.UniqueNumber == stockUniqueNumber))
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
    }
}
