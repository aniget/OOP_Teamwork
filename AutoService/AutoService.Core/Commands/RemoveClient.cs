using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using System.Linq;
using AutoService.Core.Validator;
using System.Collections.Generic;
using System;

namespace AutoService.Core.Commands
{
    public class RemoveClient : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        public RemoveClient(IDatabase database, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 2);
            var clientUniqueName = commandParameters[1];
            this.coreValidator.CounterpartyNotRegistered(this.database.Clients, clientUniqueName, "client");
            this.RemoveCounterparty(clientUniqueName, this.database.Clients);
            
        }
        private void RemoveCounterparty(string counterpartyUniqueName, IList<ICounterparty> counterparties)
        {
            ICounterparty counterparty = counterparties.FirstOrDefault(x => x.Name == counterpartyUniqueName);
            counterparties.Remove(counterparty);
            writer.Write($"{counterpartyUniqueName} removed successfully!");
        }
    }
}
