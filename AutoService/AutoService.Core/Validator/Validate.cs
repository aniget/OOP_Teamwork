using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.CustomExceptions;
using AutoService.Models.Assets;
using AutoService.Models.Contracts;

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


        public static void EmployeeExist(IList<IEmployee> employees, string employeeFirstName)
        {
            if (!employees.Any(x => x.FirstName == employeeFirstName))
            {
                throw new ArgumentException($"There is no employee called {employeeFirstName} in the AutoService");
            }
        }

        public static  IEmployee EmployeeUnique(IList<IEmployee> employees, string[] commandParameters, int employeeFirstNemaIndex, int maxLength)
        {
            IEmployee employee;

            var employeeFirstName = commandParameters[employeeFirstNemaIndex];

            if (commandParameters.Length == maxLength) //employeeFirstName + employeeLastName + employeeDepartment
            {
                var employeeLastName = commandParameters[maxLength-2];
                var employeeDepartment = commandParameters[maxLength-1];
                return employee = employees.Single(x => x.FirstName == employeeFirstName && x.LastName == employeeLastName && x.Department.ToString() == employeeDepartment);
            }
            else
            {
                if (employees.Select(x => x.FirstName == employeeFirstName).Count() > 1)
                {
                    throw new ArgumentException("More than one emplyee with same name, please provide first name, last name and department");
                }

                return employee = employees.Single(x => x.FirstName == employeeFirstName);
            }

        }
    }
}
