using System;
using System.Collections.Generic;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;

namespace AutoService.Core.Commands
{
    public class ShowEmployeesCommand : ICommand
    {
        private readonly IDatabase database;

        
        public ShowEmployeesCommand(IDatabase database)
        {
            this.database = database;
            }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            ValidateModel.EmployeeCount(this.database.Employees.Count);

            int hiredCounter = 1;
            if (this.database.Employees.Where(e => e.IsHired).Count() > 0)
            {
                Console.WriteLine("Current active employees:");
                foreach (var currentEmployee in this.database.Employees.Where(e => e.IsHired))
                {
                    Console.WriteLine(hiredCounter + ". " + currentEmployee);
                    hiredCounter++;
                }
                //int counter = 1;
            }
            else
            {
                Console.WriteLine("No active employees!");
            }

            int firedCounter = 1;

            if (this.database.Employees.Where(e => !e.IsHired).Count() > 0)
            {
                Console.WriteLine("Current fired employees:");
                foreach (var currentEmployee in this.database.Employees.Where(e => !e.IsHired))
                {

                    Console.WriteLine(firedCounter + ". " + currentEmployee.ToString());
                    firedCounter++;
                }
            }
            else
            {
                Console.WriteLine("No fired employees!");
            }
        }
    }
}
