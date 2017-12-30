using System;
using System.Security.Cryptography;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.BusinessProcess.Models;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Contracts;
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

        public BankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate)
        {
            return new BankAccount(name, responsibleEmployee, registrationDate);
        }

        public IAsset CreateStock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty supplier)
        {
            return new Stock(name, responsibleEmployee, purchasePrice, supplier);
        }

        public ISell CreateSellService(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string serviceName,
            int durationInMinutes)
        {
            return new SellService(responsibleEmployee, client, vehicle, serviceName, durationInMinutes);
        }

        public ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, IStock stock)
        {
            return new SellStock(responsibleEmployee, client, vehicle, stock);
        }

        public IOrderStock CreateOrderStock(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty supplier, IStock stock)
        {
            return new OrderStock(responsibleEmployee, price, job, supplier, stock);
        }

        public IVehicle CreateCar(string model, string make, int passengerCapacity, string registrationNumber,
            string year, EngineType engine)
        {
            IVehicle newCar = new Car(model, make, passengerCapacity, registrationNumber, year, engine);
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
