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
        private readonly IWriter writer;


        public ShowEmployees(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
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
                this.writer.Write("Current active employees:");
                foreach (var currentEmployee in this.database.Employees.Where(e => e.IsHired))
                {
                    this.writer.Write(hiredCounter + ". " + currentEmployee);
                    hiredCounter++;
                }
                //int counter = 1;
            }
            else
            {
                this.writer.Write("No active employees!");
            }

            int firedCounter = 1;

            if (this.database.Employees.Where(e => !e.IsHired).Count() > 0)
            {
                this.writer.Write("Current fired employees:");
                foreach (var currentEmployee in this.database.Employees.Where(e => !e.IsHired))
                {

                    this.writer.Write(firedCounter + ". " + currentEmployee.ToString());
                    firedCounter++;
                }
            }
            else
            {
                this.writer.Write("No fired employees!");
            }
        }
    }
}
