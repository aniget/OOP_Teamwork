using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Validator;

namespace AutoService.Core.Commands
{
    public class ChangeClientName : ICommand
    {
        //Fields
        private readonly IDatabase database;

        private readonly IAutoServiceFactory autoServiceFactory;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;

        private readonly IWriter consoleWriter;

        //Constructor
        public ChangeClientName(IDatabase database, IAutoServiceFactory autoServiceFactory, IValidateCore coreValidator, IValidateModel modelValidator, IWriter consoleWriter)
        {
            this.database = database;
            this.autoServiceFactory = autoServiceFactory;
            this.coreValidator = coreValidator;
            this.modelValidator = modelValidator;
            this.consoleWriter = consoleWriter;
        }
        //Methods
        public void ExecuteThisCommand(string[] commandParameters)
        {
            coreValidator.ExactParameterLength(commandParameters, 3);

            var clientUniqueName = commandParameters[1];
            coreValidator.CounterpartyNotRegistered(database.Clients, clientUniqueName, "client");
            var clientNewUniqueName = commandParameters[2];
            var client = database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);
            client.ChangeName(clientNewUniqueName);
            consoleWriter.Write($"Client{clientUniqueName} name changed sucessfully to {clientNewUniqueName}");
        }
    }
}
