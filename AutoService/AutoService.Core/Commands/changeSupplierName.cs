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
        private ICounterPartyManager counterPartyManager;

        //Constructor
        public ChangeSupplierName(IDatabase database, IValidateCore coreValidator, IWriter writer, ICounterPartyManager counterPartyManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.counterPartyManager = counterPartyManager;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            var supplierUniqueName = commandParameters[1];
            var supplierNewUniqueName = commandParameters[2];
            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");
            var suplier = this.database.Suppliers.FirstOrDefault(s => s.Name == supplierUniqueName);
            this.counterPartyManager.SetCounterParty(suplier);
            counterPartyManager.ChangeName(supplierNewUniqueName);
            //Printing
            writer.Write($"{supplierUniqueName} changed sucessfully to {supplierNewUniqueName}");
            
        }
    }
}
