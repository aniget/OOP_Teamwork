namespace AutoService.Models.Validator
{
    public interface IValidateModel
    {
        void InvoicePositiveAmount(decimal value);

        void InvoiceOverpaid(decimal amount, decimal value);

        int IntFromString(string commandParameter, string parameterName);

        decimal DecimalFromString(string commandParameter, string parameterName);

        void CheckNullObject(params object[] values);

        void StringForNullEmpty(params string[] values);

        void SellPrice(decimal sellPrice);

        void ServiceNameLength(string serviceName);

        void ServiceDurationInMinutes(int durationInMinutes, int minDuration, int maxDuration);

        void PassengerCapacity(int passengerCapacity);

        void MakeAndModelLength(params string[] values);

        void RegistrationNumber(string registrationNumber);

        void VehicleYear(string year);

        void HasDigitInString(string value, string parameter);

        void NonNegativeValue(decimal value, string parameter);

    }
}
