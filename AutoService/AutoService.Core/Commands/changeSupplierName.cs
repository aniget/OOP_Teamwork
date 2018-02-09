using AutoService.Core.Contracts;
using System.Linq;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ChangeSupplierName : ICommand
    {
        //Fields
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        //Constructor
        public ChangeSupplierName(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            var supplierUniqueName = commandParameters[1];
            var supplierNewUniqueName = commandParameters[2];
            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");
            var supplier = this.database.Suppliers.FirstOrDefault(s => s.Name == supplierUniqueName);
            supplier.Name = supplierNewUniqueName;
            //Printing
            writer.Write($"{supplierUniqueName} changed sucessfully to {supplierNewUniqueName}");

        }
    }
}
