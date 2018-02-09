using System.Collections.Generic;
using AutoService.Core.Contracts;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Core.Validator
{
    public interface IValidateCore
    {
        int IntFromString(string commandParameter, string parameterName);

        decimal DecimalFromString(string commandParameter, string parameterName);

        void EmployeeCount(int count);

        void ExactParameterLength(string[] parameters, int length);

        void MinimumParameterLength(string[] parameters, int length);

        void BetweenParameterLength(string[] parameters, int minLength, int maxLength);

        void EitherOrParameterLength(string[] parameters, int length1, int length2);

        void BankAccountsCount(int count);

        IEmployee EmployeeById(IList<IEmployee> employees, int id);

        IBankAccount BankAccountById(IList<IBankAccount> bankAccounts, int id);


        void EmployeeAlreadyExistOnHire(IDatabase database, string employeeFirstName, string employeeLastName,
            string employeeDepartment);

        IEmployee EmployeeUnique(IList<IEmployee> employees, string employeeFirstName, string employeeLastName,
            string employeeDepartment);

        DepartmentType DepartmentTypeFromString(string commandParameter, string department);

        VehicleType VehicleTypeFromString(string commandParameter, string vehicleType);

        EngineType EngineTypeFromString(string commandParameter, string engineType);

        void ValidateBankAccount(string bankAccount);

        void CheckNullObject(params object[] values);

        void StringForNullEmpty(params string[] values);

        void CounterpartyAlreadyRegistered(IList<ICounterparty> counterparties, string counterpartyName,
            string counterpartyType);

        void CounterpartyNotRegistered(IList<ICounterparty> counterparties, string counterpartyName,
            string counterpartyType);

        void SellPrice(decimal sellPrice);

        void ServiceNameLength(string serviceName);

        void ServiceDurationInMinutes(int durationInMinutes, int minDuration, int maxDuration);

        void InvoicePositiveAmount(decimal value);

        void InvoiceOverpaid(decimal amount, decimal value);

        void PassengerCapacity(int passengerCapacity);

        void MakeAndModelLength(params string[] values);

        void RegistrationNumber(string registrationNumber);

        void VehicleYear(string year);

        void HasDigitInString(string value, string parameter);

        void NonNegativeValue(decimal value, string parameter);

        bool IsValidResponsibilityTypeFromString(string responsibility);

        IInvoice InvoiceExists(IList<ICounterparty> clients, ICounterparty client, string invoiceNum);
    }
}
