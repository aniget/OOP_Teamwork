using System;
using System.Collections.Generic;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
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

        IAsset CreateBankAccount(string name, IEmployee responsibleEmployee, DateTime registrationDate);

        IAsset CreateStock(string name, IEmployee responsibleEmployee, decimal purchasePrice, ICounterparty vendor);

        ISell CreateSoldService(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string serviceName, int durationInMinutes);

        ISell CreateSoldStock(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, IStock stock);

        IOrder CreateOrderStock();

        IVehicle CreateCar(string model, string make, IClient owner, int passengerCapacity, string registrationNumber, string year, string engine);

        IVehicle CreateSmallTruck(string model, string make, IClient owner, string registrationNumber, string year, string engine, int weightAllowedInKilograms);

        IVehicle CreateTruck(string model, string make, IClient owner, string registrationNumber, string year, string engine, int weightAllowedInKilograms);
    }
}
