using System;
using AutoService.Core.Contracts;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Models;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Factory
{
    public class AutoServiceFactory : IAutoServiceFactory
    {
        public ICounterparty CreateClient(string name, string address, string uniqueNumber, IValidateModel modelValidator)
        {
            return new Client(name, address, uniqueNumber, modelValidator);
        }

        public ICounterparty CreateSupplier(string name, string address, string uniqueNumber, bool interfaceIsAvailable)
        {
            //throw new NotImplementedException();
            return new Supplier(name, address, uniqueNumber, interfaceIsAvailable);
        }

        public IEmployee CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department, IValidateModel modelValidator)
        {
            return new Employee(firstName, lastName, position, salary, ratePerMinute, department, modelValidator);
        }

        public IInvoice CreateInvoice(string number, DateTime date, IClient client, IValidateModel modelValidator)
        {
            return new Invoice(number, date, client, modelValidator);
        }

        public IBankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, string uniqueNumber, DateTime registrationDate)
        {
            return new BankAccount(name, responsibleEmployee, uniqueNumber, registrationDate);
        }

        public IAsset CreateStock(string name, IEmployee responsibleEmployee, string uniqueNumber, decimal purchasePrice, ICounterparty supplier)
        {
            return new Stock(name, responsibleEmployee, uniqueNumber, purchasePrice, supplier);
        }

        public ISell CreateSellService(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName,
            int durationInMinutes, IValidateModel modelValidator)
        {
            return new SellService(responsibleEmployee, client, vehicle, serviceName, durationInMinutes, modelValidator);
        }

        public ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, IStock stock, IValidateModel modelValidator)
        {
            return new SellStock(responsibleEmployee, client, vehicle, stock, modelValidator);
        }

        public IOrderStock CreateOrderStock(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock, IValidateModel modelValidator)
        {
            return new OrderStock(responsibleEmployee, supplier, stock, modelValidator);
        }

        public IVehicle CreateVehicle(string make, string model, string registrationNumber,
            string year, EngineType engine, int passengerCapacity, IValidateModel modelValidator)
        {
            IVehicle newCar = new Car(model, make, registrationNumber, year, engine, passengerCapacity, modelValidator);
            return newCar;
        }

        public IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year,
            EngineType engine, int weightAllowedInKilograms, IValidateModel modelValidator)
        {
            IVehicle newSmallTruck = new SmallTruck(model, make, registrationNumber, year, engine, weightAllowedInKilograms, modelValidator);
            return newSmallTruck;
        }

        public IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine,
            int weightAllowedInKilograms, IValidateModel modelValidator)
        {
            IVehicle newTruck = new Truck(model, make, registrationNumber, year, engine, weightAllowedInKilograms, modelValidator);
            return newTruck;
        }

        IEmployee IAutoServiceFactory.CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute, DepartmentType department, IValidateModel modelValidator)
        {
            IEmployee newEmployee = new Employee(firstName, lastName, position, salary, ratePerMinute, department, modelValidator);
            return newEmployee;
        }
    }
}
