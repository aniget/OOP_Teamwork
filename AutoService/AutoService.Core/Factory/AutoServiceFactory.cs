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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public BankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate)
        {
            return new BankAccount(name, responsibleEmployee, registrationDate);
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
        
        public IOrderStock CreateOrderStock(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty supplier, IStock stock)
        {
            return new OrderStock(responsibleEmployee, price, job, supplier, stock);
        }

        public IVehicle CreateCar(string model, string make, int passengerCapacity, string registrationNumber,
            string year, EngineType engine)
        {
            var newCar = new Car(model, make, passengerCapacity, registrationNumber, year, engine);
            return newCar;
            //throw new NotImplementedException();
        }

        public IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year,
            EngineType engine, int weightAllowedInKilograms)
        {
            var newSmallTruck = new SmallTruck(model, make, registrationNumber, year, engine, weightAllowedInKilograms);
            return newSmallTruck;
            //throw new NotImplementedException();
        }

        public IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine,
            int weightAllowedInKilograms)
        {
            var newTruck = new Truck(model, make, registrationNumber, year, engine, weightAllowedInKilograms);
            return newTruck;
            //throw new NotImplementedException();
        }
    }
}
