using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ChangeClientName : ICommand
    {
        //Fields
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;
        private ICounterPartyManager counterPartyManager;

        //Constructor
        public ChangeClientName(IDatabase database, IValidateCore coreValidator, IWriter writer, ICounterPartyManager counterPartyManager)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.counterPartyManager = counterPartyManager;
        }
        //Methods
        public void ExecuteThisCommand(string[] commandParameters)
        {
            coreValidator.ExactParameterLength(commandParameters, 3);

            var clientUniqueName = commandParameters[1];
            coreValidator.CounterpartyNotRegistered(database.Clients, clientUniqueName, "client");
            var clientNewUniqueName = commandParameters[2];
            var client = database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);
            this.counterPartyManager.SetCounterParty(client);
            counterPartyManager.ChangeName(clientNewUniqueName);
            writer.Write($"Client{clientUniqueName} name changed sucessfully to {clientNewUniqueName}");
        }
    }
}
