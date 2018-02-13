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

//sellStockToClientVehicle; Jo; 123456789; CA1234AC; RT20134HP; Manarino; Management

namespace AutoService.Core.Commands
{
    public class SellStockToClientVehicle : ICommand
    {
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IValidateModel modelValidator;
        private readonly IWriter writer;
        private readonly IStockManager stockManager;
        private readonly IAutoServiceFactory autoServiceFactory;

        public SellStockToClientVehicle(IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.database = processorLocator.GetProcessor<IDatabase>() ?? throw new ArgumentNullException();
            this.coreValidator = processorLocator.GetProcessor<IValidateCore>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
            this.stockManager = processorLocator.GetProcessor<IStockManager>() ?? throw new ArgumentNullException();
            this.autoServiceFactory = processorLocator.GetProcessor<IAutoServiceFactory>() ?? throw new ArgumentNullException();
            this.modelValidator = processorLocator.GetProcessor<IValidateModel>() ?? throw new ArgumentNullException();
        }


        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.EitherOrParameterLength(commandParameters, 5, 7);

            var employeeFirstName = commandParameters[1];
            var clientUniqueName = commandParameters[2];
            var vehicleRegistrationNumber = commandParameters[3];
            var stockUniqueNumber = commandParameters[4];
            var employeeLastName = "";

            string employeeDepartmentName = "";
            IEmployee employee;

            if (commandParameters.Length == 7)
            {
                employeeLastName = commandParameters[5];
                employeeDepartmentName = commandParameters[6];
                employee = this.coreValidator.EmployeeUnique(database.Employees, employeeFirstName, employeeLastName, employeeDepartmentName);
            }
            else
            {
                employee = this.coreValidator.EmployeeUnique(database.Employees, employeeFirstName, null, null);
            }

            this.coreValidator.CounterpartyNotRegistered(database.Clients, clientUniqueName, "client");

            var client = database.Clients.FirstOrDefault(x => x.Name == clientUniqueName);

            //stock we sell must be present in the warehouse
            bool stockExists = stockManager.ConfirmStockExists(stockUniqueNumber, employee);
            if (stockExists == false)
            {
                throw new ArgumentException(
                    $"Trying to sell the stock with unique ID {stockUniqueNumber} that is not present in the Warehouse");
            }
            var stock = this.database.AvailableStocks.FirstOrDefault(x => x.UniqueNumber == stockUniqueNumber);

            //there is no need to check vehicle for null because we create a default vehicle with every client registration
            var vehicle = ((IClient)client).Vehicles.FirstOrDefault(x =>
               x.RegistrationNumber == vehicleRegistrationNumber);

            this.SellStockToClient(stock, (IClient)client, vehicle);
        }

        private void SellStockToClient(IStock stock, IClient client, IVehicle vehicle)
        {
            if (stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Sell) ||
                stock.ResponsibleEmployee.Responsibilities.Contains(ResponsibilityType.Manage))
            {
                var sellStock = autoServiceFactory.CreateSellStock(stock.ResponsibleEmployee, client, vehicle, stock, modelValidator);

                stockManager.RemoveStockFromWarehouse(stock, stock.ResponsibleEmployee);

                //record the Sell in the notInvoicedSells Dictionary
                AddSellToNotInvoicedItems(client, sellStock);

                writer.Write($"{stock.Name} purchased from {stock.Supplier.Name} was sold to {client.Name} "
                    + $"for the amount of {sellStock.SellPrice}" + Environment.NewLine
                    + $"Employee responsible for the transaction: {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName}");

            } else throw new ArgumentException(
                $"Employee {stock.ResponsibleEmployee.FirstName} {stock.ResponsibleEmployee.LastName} does not have the required priviledges to sell stock to clients");

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
