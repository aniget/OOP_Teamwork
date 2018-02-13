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

        public ListWarehouseItems(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
            this.stockManager = processorLocator.GetProcessor<IStockManager>() ?? throw new ArgumentNullException();
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
