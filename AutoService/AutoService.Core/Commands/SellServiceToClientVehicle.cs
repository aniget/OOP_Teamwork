using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//sellServiceToClientVehicle; Jo; 123456789; CA1234AC; Disk change; 240; Manarino; Management


namespace AutoService.Core.Commands
{
    public class SellServiceToClientVehicle : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;
        private readonly IStockManager stockManager;
        private readonly IWarehouse warehouse;
        private readonly IAutoServiceFactory autoServiceFactory;

        public SellServiceToClientVehicle(IAutoServiceFactory autoServiceFactory, IDatabase database, IValidateCore coreValidator, IWriter writer, IStockManager stockManager, IWarehouse warehouse, IValidateModel modelValidator)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
            this.stockManager = stockManager;
            this.warehouse = warehouse;
            this.autoServiceFactory = autoServiceFactory;
            this.modelValidator = modelValidator;
        }


        public void ExecuteThisCommand(string[] commandParameters)
        {
            //we can sell services to client without vehicle, e.g. client brings old tire rim for repair
            this.coreValidator.EitherOrParameterLength(commandParameters, 6, 8);

            var employeeFirstName = commandParameters[1];
            var clientUniqueName = commandParameters[2];
            var vehicleRegistrationNumber = commandParameters[3];
            var serviceName = commandParameters[4];

            int durationInMinutes = this.coreValidator.IntFromString(commandParameters[5], "duration in minutes");

            var employeeLastName = string.Empty;
            IEmployee employee;

            if (commandParameters.Length == 8)
            {
                employeeLastName = commandParameters[6];
                var employeeDepartment = commandParameters[7];
                employee = this.coreValidator.EmployeeUnique(database.Employees, employeeFirstName, employeeLastName,
                    employeeDepartment);
            }
            else
            {
                employee = this.coreValidator.EmployeeUnique(database.Employees, employeeFirstName, null, null);
            }

            this.coreValidator.CounterpartyNotRegistered(database.Clients, clientUniqueName, "client");
            var client = database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);

            var vehicle = ((IClient)client).Vehicles.FirstOrDefault(x => x.RegistrationNumber == vehicleRegistrationNumber);

            this.SellServiceToClient(employee, (IClient)client, vehicle, serviceName, durationInMinutes);

        }

        private void RemoveCounterparty(string counterpartyUniqueName, IList<ICounterparty> counterparties)
        {
            ICounterparty counterparty = counterparties.FirstOrDefault(x => x.Name == counterpartyUniqueName);
            counterparties.Remove(counterparty);
            Console.WriteLine($"{counterpartyUniqueName} removed successfully!");
        }

    }
}
