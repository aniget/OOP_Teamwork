using System;
using System.Collections.Generic;
using System.Text;
using AutoService.Core.CustomExceptions;
using AutoService.Models.Assets;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Enums;

namespace AutoService.Core.Validator
{
    public static class Validate
    {
        public static int IntFromString(string commandParameter, string parameterName)
        {
            int parsedValue = int.TryParse(commandParameter, out parsedValue)
                ? parsedValue
                : throw new ArgumentException($"Please provide a valid integer value for {parameterName}!");

            return parsedValue;
        }

        public static decimal DecimalFromString(string commandParameter, string parameterName)
        {
            decimal parsedValue = decimal.TryParse(commandParameter, out parsedValue)
                ? parsedValue
                : throw new ArgumentException($"Please provide a valid decimal value for {parameterName}!");

            return parsedValue;
        }

        public static void EmployeeCount(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException("There are no employees! Do you want to hire them?");
            }
        }

        public static void ExactParameterLength(string[] parameters, int length)
        {
            if (parameters.Length != length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be exactly {length}");
            }
        }

        public static void MinimumParameterLength(string[] parameters, int length)
        {
            if (parameters.Length < length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be more than {length}");
            }
        }

        public static void BetweenParameterLength(string[] parameters, int minLength, int maxLength)
        {
            if (parameters.Length < minLength || parameters.Length > maxLength)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be between {minLength} and {maxLength}");
            }
        }

        public static void EitherOrParameterLength(string[] parameters, int length1, int length2)
        {
            if (parameters.Length == Math.Min(length1, length2) || parameters.Length == Math.Max(length1, length2))
            {
                return;
            }

            throw new ArgumentException(
                $"Parameter length for command {parameters[0]} must be either {length1} or {length2}");
        }

        public static void BankAccountsCount(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException("No bank accounts currently opened! You need to open one then deposit the cash.");
            }
        }

        public static IEmployee EmployeeById(IList<IEmployee> employees, int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException(
                    $"Please provide a valid employee value, i.e. between 1 and {employees.Count}!");
            }

            IEmployee employee = employees.Count >= id
                ? employees[id - 1]
                : throw new ArgumentException("This employee does not exist!");

            return employee;
        }

        public static BankAccount BankAccountById(IList<BankAccount> bankAccounts, int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException(
                    $"Please provide a valid bank account value, i.e. between 1 and {bankAccounts.Count}!");
            }

            BankAccount bankAccount = bankAccounts.Count >= id
                ? bankAccounts[id - 1]
                : throw new ArgumentException("This bank account does not exist!");

            return bankAccount;
        }

        public static ICounterparty ClientById(IList<ICounterparty> counterparties, int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException(
                    $"Please provide a valid counterparty id value, i.e. between 1 and {counterparties.Count}!");
            }

            ICounterparty counterparty = counterparties.Count >= id
                ? counterparties[id - 1]
                : throw new ArgumentException("This counterparty does not exist!");

            return counterparty;
        }

        public static DepartmentType DepartmentTypeFromString(string commandParameter, string department)
        {
            DepartmentType departmentFound;
            if (!Enum.TryParse(commandParameter, out departmentFound))
            {
                string[] listOfDepartments = Enum.GetNames(typeof(DepartmentType));
               
                throw new ArgumentException("Department is not valid!" + Environment.NewLine +
                    "List of departments to choose from:" + Environment.NewLine + string.Join(Environment.NewLine, listOfDepartments));
            }
            return departmentFound;
        }

        public static VehicleType VehicleTypeFromString(string commandParameter, string vehicleType)
        {
            VehicleType vehicleTypeFound;
            if (!Enum.TryParse(commandParameter, out vehicleTypeFound))
            {
                string[] listOfvehicleTypes = Enum.GetNames(typeof(VehicleType));

                throw new ArgumentException("Vehicle is not valid!" + Environment.NewLine +
                                            "List of vehicle types to choose from:" + Environment.NewLine + string.Join(Environment.NewLine, listOfvehicleTypes));
            }
            return vehicleTypeFound;
        }

        public static EngineType EngineTypeFromString(string commandParameter, string engineType)
        {
            EngineType engineTypeFound;
            if (!Enum.TryParse(commandParameter, out engineTypeFound))
            {
                string[] listOfEngineTypes = Enum.GetNames(typeof(EngineType));

                throw new ArgumentException("Engine is not valid!" + Environment.NewLine +
                                            "List of engine types to choose from:" + Environment.NewLine + string.Join(Environment.NewLine, listOfEngineTypes));
            }
            return engineTypeFound;
        }
    }
}
