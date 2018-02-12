using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System.Linq;
using System;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Models;

namespace AutoService.Core.Commands
{
    public class OrderStockToWarehouse : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateModel modelValidator;
        private readonly IAutoServiceFactory autoServiceFactory;
        private readonly IValidateCore coreValidator;
        private readonly IStockManager stockManager;
        private readonly IWriter writer;

        //Constructor
        public OrderStockToWarehouse
            (
            IDatabase database, 
            IAutoServiceFactory autoServiceFactory, 
            IValidateCore coreValidator,
            IWriter writer,
            IStockManager stockManager,
            IValidateModel modelValidator
            )
        {
            this.database = database ?? throw new ArgumentNullException();
            this.autoServiceFactory = autoServiceFactory ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.stockManager = stockManager ?? throw new ArgumentNullException();
            this.modelValidator = modelValidator ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.EitherOrParameterLength(commandParameters, 6, 8);

            string employeeFirstName = commandParameters[1];
            IEmployee employee;
            if (commandParameters.Length == 8)
            {
                employee = this.coreValidator.EmployeeUnique(this.database.Employees, employeeFirstName, commandParameters[6], commandParameters[7]);
            }
            else
            {
                employee = this.coreValidator.EmployeeUnique(this.database.Employees, employeeFirstName, null, null);
            }

            string supplierUniqueName = commandParameters[2];
            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");

            ICounterparty supplier = this.database.Suppliers.FirstOrDefault(x => x.Name == supplierUniqueName);

            string stockName = commandParameters[3];

            string stockUniqueNumber = commandParameters[4];

            decimal purchasePrice = this.coreValidator.DecimalFromString(commandParameters[5], "purchasePrice");

            IAsset stock = this.autoServiceFactory.CreateStock(stockName, employee, stockUniqueNumber, purchasePrice, supplier);

            this.OrderStockFromSupplier((IStock)stock);
            
        }

        private void OrderStockFromSupplier(IStock stock)
        {
            if (stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.BuyPartForWarehouse) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.WorkInWarehouse) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                IOrderStock orderStock = autoServiceFactory.CreateOrderStock(stock.ResponsibleEmployee, stock.Supplier, stock, modelValidator);
                if (((Supplier)stock.Supplier).InterfaceIsAvailable)
                    this.stockManager.AddStockToWarehouse(stock);
                else
                    this.stockManager.AddStockToWarehouse(stock, stock.ResponsibleEmployee);
            }
            else
            {
                throw new ArgumentException(
                    $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required repsonsibilities to register asset {stock.Name}");
            }

            writer.Write(
                $"{stock.Name} ordered from {stock.Supplier.Name} for the amount of {stock.PurchasePrice} are stored in the Warehouse." +
                Environment.NewLine +
                $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");
        }
    }
}
