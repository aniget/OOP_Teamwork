using System;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public abstract class Work : IWork
    {
        private readonly decimal price;
        
        public Work(IEmployee responsibleEmployee, decimal price, TypeOfWork job)
        {
            //Employees are validated when they are hired, so no validation here
            ResponsibleEmployee = responsibleEmployee;

            if (price < 0 || price > 1000000)
            {
                throw new ArgumentException(
                    $"Price should be a positive number less than 1,000,000. /n/r The one entered is: {price}. /n/r Please check the price and try again");
            }
            this.price = price;

            Job = job;
        }

        //Get and Set because if responsible mechanic gets sick a new responsible employee is being set on the job
        public IEmployee ResponsibleEmployee { get; set; }
        
        public decimal Price => this.price;

        public TypeOfWork Job { get; }

        public decimal GetEmployeeRatePerMinute()
        {
            //TODO: Have to complete it once there is a List Of Employees created by Engine or elsewhere
            return 0;
        }
    }
}
