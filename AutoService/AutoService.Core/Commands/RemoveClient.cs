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
using System.Collections.Generic;

namespace AutoService.Core.Commands
{
    public class RemoveClient : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IConsoleWriter wrapper;

        public RemoveClient(IDatabase database, IValidateCore coreValidator, IConsoleWriter wrapper)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.wrapper = wrapper;
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
            wrapper.Write($"{counterpartyUniqueName} removed successfully!");
        }
    }
}
