using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ListWarehouseItems : ICommand
    {
        IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;
        private readonly IStockManager stockManager;

        public ListWarehouseItems(IDatabase database, IValidateCore coreValidator, IWriter writer, IStockManager stockManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.stockManager = stockManager;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 1);
            if (this.database.AvailableStocks.Count == 0)
            {
                throw new ArgumentException("The are no avalible staocks at the Warehouse. But you can order some. ;-)");
            }
            writer.Write(this.stockManager.PrintAvailableStock());
        }
    }
}
