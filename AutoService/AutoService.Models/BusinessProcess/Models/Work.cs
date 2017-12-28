using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public abstract class Work : IWork
    {
        private readonly decimal price;

        private readonly ICollection<IEmployee> employees;

        public Work(IEmployee responsibleEmployee, decimal price, TypeOfWork job)
        {
            if (responsibleEmployee == null)
            {
                throw new ArgumentException("Invalid employee provided!");
            }
            this.ResponsibleEmployee = responsibleEmployee;

            if (price < 0 || price > 1000000)
            {
                throw new ArgumentException(
                    $"Price should be a positive number less than 1,000,000. The one entered is: {price}. Please check!");
            }
            this.price = price;

            Job = job;

        }


        public Work(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICollection<IEmployee> employees) 
            : this(responsibleEmployee, price, job)
        {
            this.employees = employees ?? throw new ArgumentException("List of employees cannot be null!");
        }



        //Get and Set because if responsible mechanic gets sick a new responsible employee is being set on the job
        public IEmployee ResponsibleEmployee { get; set; }

        public decimal Price => this.price;

        public TypeOfWork Job { get; }


        public decimal GetEmployeeRatePerMinute(IEmployee employee)
        {
            if (!employees.Contains(employee))
            {
                throw new ArgumentException("No such emplyee!");
            }
            return employees.FirstOrDefault(x => x == employee).RatePerMinute;
        }
    }
}
