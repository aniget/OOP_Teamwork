using System;
using System.Collections.Generic;
using System.Linq;
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


        public static void EmployeeExist(IList<IEmployee> employees, string employeeFirstName)
        {
            if (!employees.Any(x => x.FirstName == employeeFirstName))
            {
                throw new ArgumentException($"There is no employee called {employeeFirstName} in the AutoService");
            }
        }

        public static IEmployee EmployeeUnique(IList<IEmployee> employees, string[] commandParameters, int employeeFirstNemaIndex, int maxLength)
        {
            IEmployee employee;

            var employeeFirstName = commandParameters[employeeFirstNemaIndex];

            if (commandParameters.Length == maxLength) //employeeFirstName + employeeLastName + employeeDepartment
            {
                var employeeLastName = commandParameters[maxLength - 2];
                var employeeDepartment = commandParameters[maxLength - 1];
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

        public static ICounterparty ClientByUniqueName(IList<ICounterparty> counterparties, string clientUniqueName)
        {
            if (string.IsNullOrWhiteSpace(clientUniqueName))
            {
                throw new InvalidIdException("Please provide a valid counterparty name value");
            }

            ICounterparty counterparty = counterparties.Count(x => x.Name == clientUniqueName) == 1
                ? counterparties.FirstOrDefault(x=>x.Name == clientUniqueName)
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

        ////this is validatete in the class
        //public static void VehicleMakeModelRegNumber(string vehicleMake, string vehicleModel, string vehicleRegistrationNumber)
        //{
        //    //validate All for null
        //    if (string.IsNullOrWhiteSpace(vehicleMake) || string.IsNullOrWhiteSpace(vehicleModel) || string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
        //    {
        //        throw new ArgumentException("Vehicle parameters: null value provided for Make, Model or Registration Number!");
        //    }
        //    //validate Make
        //    if (vehicleMake.Length < 3 || vehicleMake.Length > 20) { throw new ArgumentException("Vehicle Make must be between 3 and 20 characters long"); }
        //    //validate Model
        //    if (vehicleModel.Length < 2 || vehicleModel.Length > 20) { throw new ArgumentException("Vehicle Model must be between 2 and 20 characters long"); }

        //    //validate Reg Number
        //    if (vehicleRegistrationNumber.Length < 6){ throw new ArgumentException("Invalid registration number. Must be at least 6 characters!"); }

        //}

        //public static void CounterpartyCreate(string counterpartyName, string counterpartyAddress, string counterpartyUniqueNumber)
        //{
        //    //validate All for null
        //    if (string.IsNullOrWhiteSpace(counterpartyName) || string.IsNullOrWhiteSpace(counterpartyAddress) || string.IsNullOrWhiteSpace(counterpartyUniqueNumber))
        //    {
        //        throw new ArgumentException("Vehicle parameters: null value provided for Make, Model or Registration Number!");
        //    }
        //    //validate Name
        //    if (counterpartyName.Length < 3 || counterpartyName.Length > 20) { throw new ArgumentException("Vehicle Make must be between 3 and 20 characters long"); }
        //    //validate Address
        //    if (counterpartyAddress.Length < 10 || counterpartyAddress.Length > 200) { throw new ArgumentException("Vehicle Model must be between 2 and 20 characters long"); }

        //    //validate UniqueNumber
        //    if (counterpartyUniqueNumber.Length != 9 || counterpartyUniqueNumber.Any(a => !char.IsDigit(a))) { throw new ArgumentException("Invalid unique number. Must be exactly 9 characters and only digits!"); }

        //}

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

        public static ICounterparty CounterpartyByNameOrUniqueNumber(string counterpartyNameOrUniqueNumber, IList<ICounterparty> counterparties)
        {
            if (!counterparties.Any(x => x.Name == counterpartyNameOrUniqueNumber || x.UniqueNumber == counterpartyNameOrUniqueNumber))
            {
                throw new ArgumentException($"Our AutoService does not work with supplier {counterpartyNameOrUniqueNumber}");
            }
            //iF INFO provided is not the Unique number but the name
            if (counterparties.Any(x => x.UniqueNumber != counterpartyNameOrUniqueNumber))
            {
                if (counterparties.Select(x => x.Name == counterpartyNameOrUniqueNumber).Count() > 1)
                {
                    throw new ArgumentException(
                        "More than one registered supplier with same name, please provide unique number INSTEAD of name");
                }
                else
                {
                    return counterparties.Single(x => x.Name == counterpartyNameOrUniqueNumber);
                }
            }
            else
            {
                return counterparties.Single(x => x.UniqueNumber == counterpartyNameOrUniqueNumber);
            }
        }

        public static void ValidateBankAccount(string bankAccount)
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

        //public static void CheckNullObject(object employee)
        //{
        //    throw new ArgumentException("Null object provided!");
        //}

        public static void CheckNullObject(params object[] values)
        {
            foreach (var value in values)
            {
                if (value == null)
                {
                    throw new ArgumentException("Null object provided!");
                }
            }
        }

        public static void StringForNullEmpty(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Null, empty or whistespace string provided!");
                }
            }
        }

        public static void ExistingSupplierFromNameAndUniqueNumber(IList<ICounterparty> suppliers, string supplierName, string supplierUniqueNumber)
        {
            if (suppliers.Any(a => a.Name == supplierName) || suppliers.Any(a => a.UniqueNumber == supplierUniqueNumber))
            {
                throw new ArgumentException("This supplier is already registered. If an existing supplier has changed it's name use command \"changeSupplierName\"");
            }
        }

        public static void ExistingSupplierFromUniqueNumber(IList<ICounterparty> suppliers, string supplierUniqueNumber)
        {
            if (suppliers.All(a => a.UniqueNumber != supplierUniqueNumber))
            {
                throw new ArgumentException("This supplier is not found. Please check if the correct Unique number was used!");
            }
        }

        public static void SellPrice(decimal sellPrice)
        {
            if (sellPrice < 0)
            {
                throw new ArgumentException("Sell price must be positive number");
            }
        }

        public static void ServiceNameLength(string serviceName)
        {
            if (serviceName.Length < 5 || serviceName.Length > 500) { throw new ArgumentException("ServiceName should be between 5 and 500 characters long"); }
        }

        public static void ServiceDurationInMinutes(int durationInMinutes, int minDuration, int maxDuration)
        {
            if (durationInMinutes < minDuration) { throw new ArgumentException($"As per AutoService Policy minimum duration is {minDuration} min."); }
            if (durationInMinutes > maxDuration) { throw new ArgumentException($"Duration of service should be provided in minutes and should not exceed {maxDuration} min. If the provided service took more than {maxDuration} min. please raise two sold service requests."); }
        }

        public static void InvoiceNumberLength(int numberLength)
        {
            if (numberLength != 10)
            {
                throw new ArgumentException("Invoice number must be 10 digits long!");
            }
        }

        public static void InvoicePositiveAmount(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Invoice amount must be positive!"); // to keep it simple no credit notes :)
            }
        }

        public static void InvoiceOverpaid(decimal amount, decimal value)
        {
            if (amount < value)
            {
                throw new ArgumentException("Invoice is overpaid, please correct the amount to pay!");
            }
        }
    }
}
