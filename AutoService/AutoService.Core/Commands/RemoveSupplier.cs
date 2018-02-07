using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class RemoveSupplier : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;

        public RemoveSupplier(IDatabase database, IValidateCore coreValidator)
        {
            this.database = database;
            this.coreValidator = coreValidator;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);

            string supplierUniqueName = commandParameters[1];

            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");
            this.RemoveCounterparty(supplierUniqueName, this.database.Suppliers);
        }

        private void RemoveCounterparty(string counterpartyUniqueName, IList<ICounterparty> counterparties)
        {
            ICounterparty counterparty = counterparties.FirstOrDefault(x => x.Name == counterpartyUniqueName);
            counterparties.Remove(counterparty);
            Console.WriteLine($"{counterpartyUniqueName} removed successfully!");
        }
    }
}
