using System;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Core.Contracts
{
    public interface IAutoServiceFactory
    {
        ICounterparty CreateClient(string name, string address, string uniqueNumber, IValidateModel modelValidator);

        ICounterparty CreateSupplier(string name, string address, string uniqueNumber, bool interfaceIsAvailable);

        IEmployee CreateEmployee(string firstName, string lastName, string position, decimal salary, decimal ratePerMinute,
            DepartmentType department, IValidateModel modelValidator);

        IInvoice CreateInvoice(string number, DateTime date, IClient client, IValidateModel modelValidator);

        IBankAccount CreateBankAccount(string name, IEmployee responsibleEmployee, string uniqueNumber, DateTime registrationDate);

        IAsset CreateStock(string name, IEmployee responsibleEmployee, string uniqueNumber, decimal purchasePrice, ICounterparty vendor);

        ISell CreateSellService(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, string serviceName, int durationInMinutes, IValidateModel modelValidator);

        ISell CreateSellStock(IEmployee responsibleEmployee, IClient client, IVehicle vehicle, IStock stock, IValidateModel modelValidator);

        IOrderStock CreateOrderStock(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock, IValidateModel modelValidator);

        IVehicle CreateVehicle(string make, string model, string registrationNumber, string year, EngineType engine, int passengerCapacity, IValidateModel modelValidator);

        IVehicle CreateSmallTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms, IValidateModel modelValidator);

        IVehicle CreateTruck(string model, string make, string registrationNumber, string year, EngineType engine, int weightAllowedInKilograms, IValidateModel modelValidator);
    }
}
