using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Factory
{
    public class AutoServiceFactory : IAutoServiceFactory
    {
        public ICounterparty CreateClient(string name, string address, string uniqueNumber)
        {
            throw new NotImplementedException();
        }

        public ICounterparty CreateSupplier(string name, string address, string uniqueNumber)
        {
            throw new NotImplementedException();
        }

        public IEmployee CreatEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            string department)
        {
            throw new NotImplementedException();
        }

        public IInvoice CreateInvoice(string number, DateTime date, IClient client)
        {
            throw new NotImplementedException();
        }

        public IAsset CreateBankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate)
        {
            throw new NotImplementedException();
        }

        public IAsset CreateStock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty vendor)
        {
            throw new NotImplementedException();
        }

        public ISell CreateSoldService(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string serviceName,
            int durationInMinutes)
        {
            throw new NotImplementedException();
        }

        public ISell CreateSoldStock(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, IStock stock)
        {
            throw new NotImplementedException();
        }

        public IOrder CreateOrderStock()
        {
            throw new NotImplementedException();
        }

        public IVehicle CreateCar(string model, string make, IClient owner, int passengerCapacity, string registrationNumber,
            string year, string engine)
        {
            throw new NotImplementedException();
        }

        public IVehicle CreateSmallTruck(string model, string make, IClient owner, string registrationNumber, string year,
            string engine, int weightAllowedInKilograms)
        {
            throw new NotImplementedException();
        }

        public IVehicle CreateTruck(string model, string make, IClient owner, string registrationNumber, string year, string engine,
            int weightAllowedInKilograms)
        {
            throw new NotImplementedException();
        }
    }
}
