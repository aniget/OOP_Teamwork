using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class ListWarehouseItems : ICommand
    {
        private readonly IDatabase database;
        private readonly IWarehouse warehouse;
        private readonly IValidateCore coreValidator;
        private readonly IIOWrapper wrapper;

        public ListWarehouseItems(IDatabase database, IWarehouse warehouse, IValidateCore coreValidator, IIOWrapper wrapper)
        {
            this.database = database;
            this.warehouse = warehouse;
            this.coreValidator = coreValidator;
            this.wrapper = wrapper;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 1);
            if (this.warehouse.AvailableStocks.Count == 0)
            {
                throw new ArgumentException("The are no avalible staocks at the Warehouse. But you can order it.;-)");
            }
            wrapper.WriteLineWithWrapper(this.warehouse);
        }
    }
}
