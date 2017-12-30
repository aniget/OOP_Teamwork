using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public abstract class Work : IWork
    {
        private readonly ICollection<IEmployee> employees;
        private decimal price;

        public Work(IEmployee responsibleEmployee, decimal price, TypeOfWork job)
        {
            if (responsibleEmployee == null)
            {
                throw new ArgumentException("Invalid employee provided!");
            }
            this.ResponsibleEmployee = responsibleEmployee;

            Job = job;

            this.price = price;

        }

        public decimal Price => this.price;

        public Work(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICollection<IEmployee> employees) 
            : this(responsibleEmployee, price, job)
        {
            this.employees = employees ?? throw new ArgumentException("List of employees cannot be null!");
        }



        //Get and Set because if responsible mechanic gets sick a new responsible employee is being set on the job
        public IEmployee ResponsibleEmployee { get; set; }

        
        public TypeOfWork Job { get; protected set; }


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
