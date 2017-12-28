using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICollection<IInvoice> invoices;
        private readonly ICollection<IEmployee> employees;
        private readonly ICollection<IVehicle> vehicles;
        private readonly ICollection<IAsset> assets;
        private readonly ICollection<ICounterparty> clients;
        private readonly ICollection<ICounterparty> vendors;

        private static readonly IEngine SingleInstance = new Engine();

        private Engine()
        {
            this.invoices = new List<IInvoice>();
            this.employees = new List<IEmployee>();
            this.vehicles = new List<IVehicle>();
            this.assets = new List<IAsset>();
            this.clients = new List<ICounterparty>();
            this.vendors = new List<ICounterparty>();
        }

        public static IEngine Instance
        {
            get
            {
                return SingleInstance;
            }
        }

        public void Run()
        {
            var command = ReadCommand();
            var commandParameters = new string[] { string.Empty };

            while (command != "exit")
            {
                commandParameters = ParseCommand(command);
                try
                {
                    ExecuteCommand(commandParameters);
                }
                catch (NotSupportedException e)
                {
                    Console.Write(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                command = ReadCommand();
            }
        }

        public string ReadCommand()
        {
            return Console.ReadLine();
        }

        public string[] ParseCommand(string command)
        {
            return command.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void ExecuteCommand(string[] commandParameters)
        {
            // Choose strategy
            var commandType = commandParameters[0];
            //switch (commandType)
            //{
            //    case GlobalConstants.CreationStrategyCommand:
            //        this.ExecuteCreationStrategy(commandParameters);
            //        break;
            //    case GlobalConstants.RemovalStrategyCommand:
            //        this.ExecuteRemovalStrategy(commandParameters);
            //        break;
            //    case GlobalConstants.AssigningStrategyCommand:
            //        this.ExecuteAssigningStrategy(commandParameters);
            //        break;
            //    case GlobalConstants.SelectingStrategyCommand:
            //        this.ExecuteSelectingStrategy(commandParameters);
            //        break;
            //    case GlobalConstants.RunningStrategyCommand:
            //        this.ExecuteRunningStrategy(commandParameters);
            //        break;
            //    case GlobalConstants.DisplayingStrategyCommand:
            //        this.ExecuteDisplayingStrategy(commandParameters);
            //        break;
            //    default:
            //        throw new InvalidOperationException();
            //}
        }
    }
}
