using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Models;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Core.Manager
{
    public class StockManager : IStockManager
    {
        private IWarehouse warehouse;
        private IDatabase database;
        private readonly IValidateModel modelValidator;

        //DI: initialize the singleInstance objects that are used in the methods
        //properties not needed for now
        public StockManager(IWarehouse warehouse, IDatabase database, IValidateModel modelValidator)
        {
            this.warehouse = warehouse;
            this.database = database;
            this.modelValidator = modelValidator;
        }


        public IValidateModel ModelValidator { get => this.modelValidator; }

        //once ordered the stock is automatically added (without Employee intervention) to warehouse 
        //thanks to integration of our application with the supplier's system
        public void AddStockToWarehouse(IStock stock/*, IWarehouse warehouse, ICounterparty supplier*/)
        {
            //if (((Supplier)supplier).InterfaceIsAvailable)
                warehouse.AvailableStocks.Add(stock);
            //else
            //    throw new ArgumentException
            //        ("This order cannot be recorded automatically because there is no system interface with this supplier");
        }

        //stock is recorded in warehouse by authorized employee
        public void AddStockToWarehouse(IStock stock, IEmployee employee/*, IWarehouse warehouse*/)
        {
            this.ModelValidator.CheckNullObject(stock, employee);
            //only employees with right (Responsibility) to BUY can perform this work
            //if (employee.Responsibilities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
            //                    employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
            //                    employee.Responsibilities.Contains(ResponsibilityType.BuyPartForClient) ||
            //                    employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))
            //{
            warehouse.AvailableStocks.Add(stock);
            //}
            //else
            //{
            //    throw new ArgumentException("No authorization to put stock in warehouse parts.");
            //}
        }

        //stock recorded directly to the client (skip warehouse)
        public void AddStockToClient(IStock stock, IEmployee employee, IClient client, IVehicle vehicle/*, IDatabase database*/)
        {
            //record the Sell in the notInvoicedSells Dictionary of the specified client and vehicle
            if (!database.NotInvoicedSales.ContainsKey(client))
            {
                database.NotInvoicedSales[client] = new List<ISell>();
            }
            ISell sell = new SellStock(employee, client, vehicle, stock, modelValidator); /*  { Client = client, Vehicle = vehicle, stock.PurchasePrice };*/
            database.NotInvoicedSales[client].Add(sell);
        }

        public void RemoveStockFromWarehouse(IStock stock, IEmployee employee/*, IWarehouse warehouse*/)
        {
            this.ModelValidator.CheckNullObject(stock, employee);

            //only employees with right (Responsibility) to SELL can perform this work
            //if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
            //    employee.Responsibilities.Contains(ResponsibilityType.Manage))

            warehouse.AvailableStocks.Remove(stock);
            //else
            //{
            //    throw new ArgumentException("No authorization to remove stock from warehouse!");
            //}
        }

        public bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee/*, IWarehouse warehouse*/)
        {
            this.ModelValidator.CheckNullObject(stockUniqueNumber, employee);
            bool exists = false;
            //only employees with right (Responsibility) to SELL can perform this work
            //if (employee.Responsibilities.Contains(ResponsibilityType.Sell) ||
            //    employee.Responsibilities.Contains(ResponsibilityType.Manage) ||
            //    employee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse))
            //{
            if (warehouse.AvailableStocks.Any(x => x.UniqueNumber == stockUniqueNumber))
            {
                exists = true;
            }
            //}
            //else
            //{
            //    throw new ArgumentException("No authorization to check stock in warehouse!");
            //}
            return exists;
        }
    }
}
