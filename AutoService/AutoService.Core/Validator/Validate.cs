using System;

namespace AutoService.Core.Validator
{
    public static class Validate
    {
        //public static int ValidateIntFromString(string parameter)
        //{
        //    return 5;
        //}

       public static decimal ValidateDecimalFromString(string commandParameter)
        {
            decimal salary = decimal.TryParse(commandParameter, out salary)
                ? salary
                : throw new ArgumentException("Please provide a valid decimal value for salary!");

            return salary;
        }
    }
}
