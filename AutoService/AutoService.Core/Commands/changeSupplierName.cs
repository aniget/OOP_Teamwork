﻿using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System.Linq;
using System;
using AutoService.Core.Validator;

namespace AutoService.Core.Commands
{
    public class ChangeSupplierName : ICommand
    {
        //Fields
        private readonly IDatabase database;
        private readonly IValidateCore coreValidator;
        private readonly IConsoleWriter writer;
        //Constructor
        public ChangeSupplierName(IDatabase database, IValidateCore coreValidator, IConsoleWriter writer)
        {
            this.database = database;
            this.coreValidator = coreValidator;
            this.writer = writer;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            this.coreValidator.ExactParameterLength(commandParameters, 3);

            var supplierUniqueName = commandParameters[1];
            var supplierNewUniqueName = commandParameters[2];
            this.coreValidator.CounterpartyNotRegistered(this.database.Suppliers, supplierUniqueName, "supplier");
            var suplier = this.database.Suppliers.FirstOrDefault(s => s.Name == supplierUniqueName);
            suplier.ChangeName(supplierNewUniqueName);
            //Printing
            writer.Write(($"{supplierUniqueName} changed sucessfully to {supplierNewUniqueName}"));
            
        }
    }
}
