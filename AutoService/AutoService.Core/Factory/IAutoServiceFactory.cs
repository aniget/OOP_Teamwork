using System;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Core.Factory
{
    public interface IAutoServiceFactory
    {
        ICounterparty CreateClient(string name, string address, string uniqueNumber);

        ICounterparty CreateSupplier(string name, string address, string uniqueNumber);

        IEmployee CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department);

        IInvoice CreateInvoice(string number, DateTime date, IClient client);

        BankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, string uniqueNumber, DateTime registrationDate);

        IAsset CreateStock(string name, IEmployee responsibleEmployee, string uniqueNumber, decimal purchasePrice, ICounterparty vendor);

        ISell CreateSellService(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName, int durationInMinutes);

        ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, IStock stock);

        IOrderStock CreateOrderStock(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock);

        IVehicle CreateVehicle(string make, string model, string registrationNumber, string year, EngineType engine, int passengerCapacity);

        IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms);

        IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms);
    }
}
