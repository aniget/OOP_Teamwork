using AutoService.Core.Contracts;
using System;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class ChangeSupplierName : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public ChangeSupplierName(IProcessorLocator processorLocator)
        {
            if (processorLocator == null)throw new ArgumentNullException();
this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);
            var supplierUniqueName = commandParameters[1];
            var supplierNewUniqueName = commandParameters[2];
            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");
            var supplier = this.database.Suppliers.FirstOrDefault(s => s.Name == supplierUniqueName);
            supplier.Name = supplierNewUniqueName;

            writer.Write($"{supplierUniqueName} changed sucessfully to {supplierNewUniqueName}");

        }
    }
}
