using System;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ShowEmployees : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;


        public ShowEmployees(IDatabase database, IValidateCore coreValidator)
        {
            this.database = database;
            this.coreValidator = coreValidator;
        }

        public IValidateCore CoreValidator
        {
            get => this.coreValidator;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.CoreValidator.EmployeeCount(this.database.Employees.Count);

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
