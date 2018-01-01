using System;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Models;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Factory
{
    public class AutoServiceFactory : IAutoServiceFactory
    {
        public ICounterparty CreateClient(string name, string address, string uniqueNumber)
        {
            return new Client(name, address, uniqueNumber);
        }

        public ICounterparty CreateSupplier(string name, string address, string uniqueNumber)
        {
            return new Supplier(name, address, uniqueNumber);
        }

        public IEmployee CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department)
        {
            return new Employee(firstName, lastName, position, salary, ratePerMinute, department);
        }

        public IInvoice CreateInvoice(string number, DateTime date, IClient client)
        {
            return new Invoice(number, date, client);
        }

        public BankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, string uniqueNumber, DateTime registrationDate)
        {
            return new BankAccount(name, responsibleEmployee, uniqueNumber, registrationDate);
        }

        public IAsset CreateStock(string name, IEmployee responsibleEmployee, string uniqueNumber, decimal purchasePrice, ICounterparty supplier)
        {
            return new Stock(name, responsibleEmployee, uniqueNumber, purchasePrice, supplier);
        }

        public ISell CreateSellService(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName,
            int durationInMinutes)
        {
            return new SellService(responsibleEmployee, client, vehicle, serviceName, durationInMinutes);
        }

        public ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, IStock stock)
        {
            return new SellStock(responsibleEmployee, client, vehicle, stock);
        }

        public IOrderStock CreateOrderStock(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock)
        {
            return new OrderStock(responsibleEmployee, supplier, stock);
        }

        public IVehicle CreateVehicle(string make, string model, string registrationNumber,
            string year, EngineType engine, int passengerCapacity)
        {
            IVehicle newCar = new Car(model, make, registrationNumber, year, engine, passengerCapacity);
            return newCar;
        }

        public IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year,
            EngineType engine, int weightAllowedInKilograms)
        {
            IVehicle newSmallTruck = new SmallTruck(model, make, registrationNumber, year, engine, weightAllowedInKilograms);
            return newSmallTruck;
        }

        public IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine,
            int weightAllowedInKilograms)
        {
            IVehicle newTruck = new Truck(model, make, registrationNumber, year, engine, weightAllowedInKilograms);
            return newTruck;
        }
    }
}
