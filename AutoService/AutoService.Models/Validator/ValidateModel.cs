using System;
using System.Linq;

namespace AutoService.Models.Validator
{
    public class ValidateModel : IValidateModel
    {
        public ValidateModel()
        {
            
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
    }
}