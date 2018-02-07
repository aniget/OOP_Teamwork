using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class ListWarehouseItems : ICommand
    {
        private readonly IWarehouse warehouse;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public ListWarehouseItems(IWarehouse warehouse, IValidateCore coreValidator, IWriter writer)
        {
            this.warehouse = warehouse;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 1);
            if (this.warehouse.AvailableStocks.Count == 0)
            {
                throw new ArgumentException("The are no avalible staocks at the Warehouse. But you can order some. ;-)");
            }
            writer.Write(this.warehouse.ToString());
        }
    }
}
