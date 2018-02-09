using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Manager
{
    public class CounterPartyManager : ICounterPartyManager
    {
        private readonly IValidateCore coreValidator;
        private ICounterparty counterparty;
        private IClient client;

        public CounterPartyManager(IValidateCore coreValidator)
        {
            this.coreValidator = coreValidator;
        }

        public void SetCounterParty(ICounterparty counterparty)
        {
            this.counterparty = counterparty;
            this.client = (IClient) counterparty;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.counterparty = (IClient) this.counterparty;
            this.coreValidator.CheckNullObject(vehicle);
            this.client.Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this.coreValidator.CheckNullObject(vehicle);
            this.client.Vehicles.Remove(vehicle);
        }

        public void UpdateDueDays(int dueDays)
        {
            this.client.DueDaysAllowed = dueDays;
        }

        public void ChangeName(string name)
        {
            this.counterparty.Name = name;
        }
    }
}
