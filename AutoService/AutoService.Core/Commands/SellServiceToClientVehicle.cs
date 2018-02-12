using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

//sellServiceToClientVehicle; Jo; 123456789; CA1234AC; Disk change; 240; Manarino; Management


namespace AutoService.Core.Commands
{
    public class SellServiceToClientVehicle : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;
        private readonly IAutoServiceFactory autoServiceFactory;

        public SellServiceToClientVehicle(IAutoServiceFactory autoServiceFactory, IDatabase database, IValidateCore coreValidator, IWriter writer, IValidateModel modelValidator)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.autoServiceFactory = autoServiceFactory ?? throw new ArgumentNullException();
            this.modelValidator = modelValidator ?? throw new ArgumentNullException();
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

       private void SellServiceToClient(IEmployee responsibleEmployee, IClient client, IVehicle vehicle,
            string serviceName, int durationInMinutes)
        {
            ISellService sellService;
            if (responsibleEmployee.Responsibilities.Contains(ResponsibilityType.Repair) ||
                responsibleEmployee.Responsibilities.Contains(ResponsibilityType.SellService))
            {
                sellService = (ISellService)autoServiceFactory.CreateSellService(responsibleEmployee, client, vehicle, serviceName, durationInMinutes, modelValidator);

                //record the Sell in the notInvoicedSells Dictionary
                AddSellToNotInvoicedItems(client, sellService);

            }
            else
            {
                throw new ArgumentException(
                    $"Employee {responsibleEmployee.FirstName} {responsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");
            }

            writer.Write(
                $"{serviceName} was performed to {client.Name} for the amount of {sellService.SellPrice}" +
                Environment.NewLine
                + $"Employee responsible for the repair: {responsibleEmployee.FirstName} {responsibleEmployee.LastName}");
        }

        private void AddSellToNotInvoicedItems(IClient client, ISell sell)
        {
            if (!database.NotInvoicedSales.ContainsKey(client))
            {
                database.NotInvoicedSales[client] = new List<ISell>();
            }
            database.NotInvoicedSales[client].Add(sell);
        }


    }
}
