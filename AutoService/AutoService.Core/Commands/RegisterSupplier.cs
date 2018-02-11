using System;
using System.Linq;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class RegisterSupplier : ICommand
    {
        private readonly IDatabase database;
        private readonly IAutoServiceFactory factory;
        private readonly IValidateCore coreValidator;
        private readonly IWriter writer;


        public RegisterSupplier(IDatabase database, IAutoServiceFactory factory, IValidateCore coreValidator, IWriter writer)
        {
            this.database = database ?? throw new ArgumentNullException();
            this.factory = factory ?? throw new ArgumentNullException();
            this.coreValidator = coreValidator ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.EitherOrParameterLength(commandParameters, 4, 5);

            string supplierUniqueName = commandParameters[1];
            string supplierAddress = commandParameters[2];
            string supplierUniqueNumber = commandParameters[3];

            ////TODO: add to validate class once refactored
            //bool interfaceIsAvailable;
            //if (commandParameters[4] is null)
            //    interfaceIsAvailable = false;
            //else
            //    interfaceIsAvailable = bool.Parse(commandParameters[4]);

            this.coreValidator.CounterpartyAlreadyRegistered(this.database.Suppliers, supplierUniqueName, "supplier");

            this.AddSupplier(supplierUniqueName, supplierAddress, supplierUniqueNumber);
            this.writer.Write("Supplier registered sucessfully");
        }

        private void AddSupplier(string name, string address, string uniqueNumber, bool interfaceIsAvailable = false)
        {
            ICounterparty supplier = this.factory.CreateSupplier(name, address, uniqueNumber, interfaceIsAvailable);

            var sameSupplierFound = this.database.Suppliers.FirstOrDefault(x => x.UniqueNumber == uniqueNumber);
            if (sameSupplierFound != null)
            {
                throw new ArgumentException(
                    "Supplier with the same unique number already exist. Please check the number and try again!");
            }
            this.database.Suppliers.Add(supplier);
            
            this.writer.Write($"Supplier {name} added successfully with Id {this.database.Suppliers.Count}!");
        }
    }
}
