using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;
using AutoService.Models.Vehicles.Models;
using System;
using System.Linq;

namespace AutoService.Core.Commands
{
    public class AddVehicleToClient : ICommand
    {
        private readonly IDatabase database;
        private readonly IWriter writer;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IAutoServiceFactory factory;

        public AddVehicleToClient(IDatabase database, IWriter writer, IValidateCore coreValidator, IAutoServiceFactory factory, IValidateModel modelValidator)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.modelValidator = modelValidator ?? throw new ArgumentNullException();
            this.factory = factory ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 9);

            var vehicleType = this.coreValidator.VehicleTypeFromString(commandParameters[1], "vehicle type");
            var vehicleMake = commandParameters[2];
            var vehicleModel = commandParameters[3];
            var vehicleRegistrationNumber = commandParameters[4];
            var vehicleYear = commandParameters[5];
            var engineType = this.coreValidator.EngineTypeFromString(commandParameters[6], "engine type");
            var additionalParams = this.coreValidator.IntFromString(commandParameters[7], "additional parameters");

            this.coreValidator.CounterpartyNotRegistered(this.database.Clients, commandParameters[8], "client");

            var clientUniqueName = commandParameters[8];

            var client = this.database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);

            if (((IClient)client).Vehicles.Any(x => x.RegistrationNumber == vehicleRegistrationNumber))
            {
                throw new ArgumentException(
                    $"This client already has a vehicle with this registration number: {vehicleRegistrationNumber}.");
            }
            IVehicle newVehicle = this.CreateVehicle(vehicleType, vehicleMake, vehicleModel, vehicleRegistrationNumber,
                vehicleYear, engineType, additionalParams);

            ((IClient)client).Vehicles.Add((Vehicle)newVehicle);
            this.writer.Write(newVehicle.ToString());

            this.writer.Write($"Vehicle {vehicleMake} {vehicleModel} added to client {client.Name}");
        }

        private IVehicle CreateVehicle(VehicleType vehicleType, string vehicleMake, string vehicleModel,
                string registrationNumber, string vehicleYear, EngineType engineType, int additionalParams)
        {
            IVehicle vehicle = null;

            if (vehicleType == VehicleType.Car)
            {
                vehicle = this.factory.CreateVehicle(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams, modelValidator);
            }
            else if (vehicleType == VehicleType.SmallTruck)
            {
                vehicle = this.factory.CreateSmallTruck(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams, modelValidator);
            }
            else if (vehicleType == VehicleType.Truck)
            {
                vehicle = this.factory.CreateTruck(vehicleModel, vehicleMake, registrationNumber,
                    vehicleYear, engineType, additionalParams, modelValidator);
            }
            return vehicle;
        }
    }
}
