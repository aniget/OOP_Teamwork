using System;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Core.Factory
{
    public interface IAutoServiceFactory
    {
        ICounterparty CreateClient(string name, string address, string uniqueNumber);

        ICounterparty CreateSupplier(string name, string address, string uniqueNumber);

        IEmployee CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department);

        IInvoice CreateInvoice(string number, DateTime date, IClient client);

        BankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate);

        IAsset CreateStock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty vendor);

        ISell CreateSellService(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string serviceName, int durationInMinutes);

        ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, IStock stock);

        IOrderStock CreateOrderStock(IEmployee responsibleEmployee, decimal purchasePrice, TypeOfWork job, ICounterparty supplier, IStock stock);

        IVehicle CreateCar(string model, string make, string registrationNumber, string year, EngineType engine, int passengerCapacity);

        IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms);

        IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms);
    }
}
