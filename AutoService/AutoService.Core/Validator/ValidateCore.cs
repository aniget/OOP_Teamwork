using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.CustomExceptions;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Enums;

//testing branches
//test 2

namespace AutoService.Core.Validator
{
    public class ValidateCore : IValidateCore
    {
        private readonly IWriter writer;

        public ValidateCore(IWriter writer)
        {
            this.writer = writer;
        }

        

        public int IntFromString(string commandParameter, string parameterName)
        {
            int parsedValue = int.TryParse(commandParameter, out parsedValue)
                ? parsedValue
                : throw new ArgumentException($"Please provide a valid integer value for {parameterName}!");

            return parsedValue;
        }

        public decimal DecimalFromString(string commandParameter, string parameterName)
        {
            decimal parsedValue = decimal.TryParse(commandParameter, out parsedValue)
                ? parsedValue
                : throw new ArgumentException($"Please provide a valid decimal value for {parameterName}!");

            return parsedValue;
        }

        public void EmployeeCount(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException("There are no employees! Do you want to hire any?");
            }
        }

        public void ExactParameterLength(string[] parameters, int length)
        {
            if (parameters.Length != length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be exactly {length}");
            }
        }

        public void MinimumParameterLength(string[] parameters, int length)
        {
            if (parameters.Length < length)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be more than {length}");
            }
        }

        public void BetweenParameterLength(string[] parameters, int minLength, int maxLength)
        {
            if (parameters.Length < minLength || parameters.Length > maxLength)
            {
                throw new ArgumentException(
                    $"Parameter length for command {parameters[0]} must be between {minLength} and {maxLength}");
            }
        }

        public void EitherOrParameterLength(string[] parameters, int length1, int length2)
        {
            if (parameters.Length == Math.Min(length1, length2) || parameters.Length == Math.Max(length1, length2))
            {
                return;
            }

            throw new ArgumentException(
                $"Parameter length for command {parameters[0]} must be either {length1} or {length2}");
        }

        public void BankAccountsCount(int count)
        {
            if (count == 0)
            {
                throw new ArgumentException("No bank accounts currently opened! You need to open one then deposit the cash.");
            }
        }

        public IEmployee EmployeeById(IList<IEmployee> employees, int id)
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

        public BankAccount BankAccountById(IList<BankAccount> bankAccounts, int id)
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

        //TODO: to be completed after Validate class is refactored 
        public void EmployeeAlreadyExistOnHire(IDatabase database, string employeeFirstName, string employeeLastName, string employeeDepartment)
        {
            if (database.Employees.Any(x => x.FirstName == employeeFirstName && x.LastName == employeeLastName && x.Department.ToString() == employeeDepartment))
            {
                throw new ArgumentException($"There is already employee called {employeeFirstName} in the AutoService");
            }
        }

        public IEmployee EmployeeUnique(IList<IEmployee> employees, string employeeFirstName, string employeeLastName, string employeeDepartment)
        {
            IEmployee employee;

            if (!(string.IsNullOrWhiteSpace(employeeLastName) && string.IsNullOrEmpty(employeeDepartment))) //employeeFirstName + employeeLastName + employeeDepartment
            {
                if (employees.Count(x => x.FirstName == employeeFirstName && x.LastName == employeeLastName && x.Department.ToString() == employeeDepartment) > 1)
                {
                    throw new ArgumentException("Employees with equal First Name, Last name and Department are not allowed! Please rename one of your employees and buy him/her a shirt!");
                }
                else if (employees.Count(x => x.FirstName == employeeFirstName && x.LastName == employeeLastName && x.Department.ToString() == employeeDepartment) == 1)
                {
                    return employee = employees.Single(x => x.FirstName == employeeFirstName && x.LastName == employeeLastName && x.Department.ToString() == employeeDepartment);
                }
                // count = 0
                throw new ArgumentException("There is no employee with this name");
            }

            if (employees.Count(x => x.FirstName == employeeFirstName) > 1)
            {
                throw new ArgumentException("More than one employee with the same name, please provide first name, last name and department");
            }

            return employee = employees.Single(x => x.FirstName == employeeFirstName);
        }

        public DepartmentType DepartmentTypeFromString(string commandParameter, string department)
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

        public VehicleType VehicleTypeFromString(string commandParameter, string vehicleType)
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

        public EngineType EngineTypeFromString(string commandParameter, string engineType)
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

        public void ValidateBankAccount(string bankAccount)
        {
            //Special thanks for the ValidateBankAccount code to
            //https://www.codeproject.com/Tips/775696/IBAN-Validator

            if (String.IsNullOrEmpty(bankAccount))
            {
                throw new ArgumentException("Bank Account IBAN cannot be null!");
            }

            bankAccount = bankAccount.Replace(" ", String.Empty).ToUpper();

            if (System.Text.RegularExpressions.Regex.IsMatch(bankAccount,
                @"^[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}$"))
            {
                bankAccount = bankAccount.Substring(4) + bankAccount.Substring(0, 4);
                int checksum = 0;
                foreach (char c in bankAccount)
                {
                    if (Char.IsLetter(c))
                    {
                        checksum = ((100 * checksum) + (c - 55)) % 97;
                    }
                    else
                    {
                        checksum = ((10 * checksum) + (c - 48)) % 97;
                    }
                }

                if (checksum != 1)
                    throw new ArgumentException("Bank Account IBAN is invalid!");
            }
            else
            {
                throw new ArgumentException("Bank Account IBAN is invalid!");
            }
        }

        public void CheckNullObject(params object[] values)
        {
            foreach (var value in values)
            {
                if (value == null)
                {
                    throw new ArgumentException("Null object provided!");
                }
            }
        }

        public void StringForNullEmpty(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Null, empty or whistespace string provided!");
                }
            }
        }

        public void CounterpartyAlreadyRegistered(IList<ICounterparty> counterparties, string counterpartyName, string counterpartyType)
        {
            if (string.IsNullOrWhiteSpace(counterpartyName))
            {
                throw new InvalidIdException($"Please provide a valid {counterpartyType} name");
            }

            if (counterparties.Any(a => a.Name == counterpartyName))
            {
                throw new ArgumentException($"{counterpartyName} is already registered. If an existing counterparty has changed it's name use command \"change{counterpartyType}Name\"");
            }
        }

        public void CounterpartyNotRegistered(IList<ICounterparty> counterparties, string counterpartyName, string counterpartyType)
        {
            this.StringForNullEmpty(counterpartyName);

            if (counterparties.All(a => a.Name != counterpartyName))
            {
                throw new ArgumentException($"{counterpartyType} is not registered.");
            }
        }

        public void SellPrice(decimal sellPrice)
        {
            this.NonNegativeValue(sellPrice, "sell price");
        }

        public void ServiceNameLength(string serviceName)
        {
            if (serviceName.Length < 5 || serviceName.Length > 500) { throw new ArgumentException("ServiceName should be between 5 and 500 characters long"); }
        }

        public void ServiceDurationInMinutes(int durationInMinutes, int minDuration, int maxDuration)
        {
            if (durationInMinutes < minDuration) { throw new ArgumentException($"As per AutoService Policy minimum duration is {minDuration} min."); }
            if (durationInMinutes > maxDuration) { throw new ArgumentException($"Duration of service should be provided in minutes and should not exceed {maxDuration} min. If the provided service took more than {maxDuration} min. please raise two sold service requests."); }
        }

        public void InvoicePositiveAmount(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Invoice amount must be positive!"); // to keep it simple no credit notes :)
            }
        }

        public void InvoiceOverpaid(decimal amount, decimal value)
        {
            if (amount < value)
            {
                throw new ArgumentException("Invoice is overpaid, please correct the amount to pay!");
            }
        }

        public void PassengerCapacity(int passengerCapacity)
        {
            if (passengerCapacity < 1 || passengerCapacity > 9)
            {
                throw new ArgumentException("Invalid passenger count for car!");
            }
        }

        public void MakeAndModelLength(params string[] values)
        {
            foreach (var value in values)
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("Make ot Model should not be more than 50 characters long!");
                }
            }
        }

        public void RegistrationNumber(string registrationNumber)
        {
            if (registrationNumber.Length < 6)
            {
                throw new ArgumentException("Invalid registration number. Must be at least 6 characters!");
            }
        }

        public void VehicleYear(string year)
        {
            if (year.Any(a => !char.IsDigit(a)))
            {
                throw new ArgumentException("Invalid year!");
            }

            this.IntFromString(year, "Vehicle Year");
            if (int.Parse(year) < 1900 || int.Parse(year) > DateTime.Now.Year)
            {
                throw new ArgumentException($"Invalid year! Cars are produced between 1900 and {DateTime.Now.Year}");
            }
        }

        public void HasDigitInString(string value, string parameter)
        {
            if (value.Any(char.IsDigit))
            {
                throw new ArgumentException($"Invalid value! Cannot have digits in parameter {parameter}");
            }
        }

        public void NonNegativeValue(decimal value, string parameter)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{parameter} cannot be negative!");
            }
        }

        public bool IsValidResponsibilityTypeFromString(string responsibility)
        {
            bool isValid = true;
            ResponsibilityType currentResponsibility;
            if (!Enum.TryParse(responsibility, out currentResponsibility))
            {
                writer.Write($"Responsibility {responsibility} not valid!");
                isValid = false;
            }
            return isValid;
        }

        public IInvoice InvoiceExists(IList<ICounterparty> clients, ICounterparty client, string invoiceNum)
        {
            ICounterparty clientFound = clients.FirstOrDefault(fd => fd.UniqueNumber == client.UniqueNumber);
            IInvoice invoice = clientFound.Invoices.FirstOrDefault(fd => fd.Number == invoiceNum);
            if (invoice == null)
            {
                throw new ArgumentException("Invoice does not exist!");
            }
            return invoice;
        }
    }
}