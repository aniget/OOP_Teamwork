using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Commands
{
    public class RegisterClient : ICommand
    {
        private readonly IDatabase database;

        private readonly IAutoServiceFactory autoServiceFactory;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;

        private readonly IWriter writer;

        public RegisterClient(IDatabase database, IAutoServiceFactory autoServiceFactory, IValidateCore coreValidator, IValidateModel modelValidator, IWriter writer)
        {
            this.database = database;
            this.autoServiceFactory = autoServiceFactory;
            this.coreValidator = coreValidator;
            this.modelValidator = modelValidator;
            this.writer = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            coreValidator.ExactParameterLength(commandParameters, 4);

            var clientUniqueName = commandParameters[1];
            var clientAddress = commandParameters[2];
            var clientUniquieNumber = commandParameters[3];

            coreValidator.CounterpartyAlreadyRegistered(this.database.Clients, clientUniqueName, "client");

            var client = (IClient)autoServiceFactory.CreateClient(clientUniqueName, clientAddress, clientUniquieNumber, modelValidator);

            IVehicle vehicle = autoServiceFactory.CreateVehicle("default", "default", "AA0000AA", "2000", Models.Vehicles.Enums.EngineType.Petrol, 5, modelValidator);

            this.coreValidator.CheckNullObject(vehicle);

            //add Vehicle to Client's vehicles
            client.Vehicles.Add(vehicle);

            //add Client to database
            database.Clients.Add(client);

            writer.Write(client.ToString());
            writer.Write($"Client {clientUniqueName} added successfully with unique number {clientUniquieNumber}");
            writer.Write($"Default Vehicle added to client {client.Name}");
        }
    }
}
