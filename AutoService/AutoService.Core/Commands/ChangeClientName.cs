using AutoService.Core.Contracts;
using System;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class ChangeClientName : ICommand
    {
        //Fields
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;

        //Constructor
        public ChangeClientName(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
        }
        //Methods
        public void ExecuteThisCommand(string[] commandParameters)
        {
            coreValidator.ExactParameterLength(commandParameters, 3);

            var clientUniqueName = commandParameters[1];
            coreValidator.CounterpartyNotRegistered(database.Clients, clientUniqueName, "client");
            var clientNewUniqueName = commandParameters[2];
            var client = database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);
            client.Name = clientNewUniqueName;
            writer.Write($"Client{clientUniqueName} name changed sucessfully to {clientNewUniqueName}");
        }
    }
}
