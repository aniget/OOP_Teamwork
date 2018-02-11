using AutoService.Core.Commands;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Tests.CommandsTests
{
    [TestClass]
    public class FireEmployee_Should
    {
        [TestMethod]
        public void ThrowArgumentException_IfEmployeeIsAlreadyFired()
        {
            //fireEmployee;1


            var databaseStub = new Mock<IDatabase>();
            var coreValidatorStub = new Mock<IValidateCore>();
            var writerStub = new Mock<IWriter>();

            var sut = new FireEmployee(databaseStub.Object, coreValidatorStub.Object, writerStub.Object);
        //    sut.ExecuteThisCommand()

        //public void ExecuteThisCommand(string[] commandParameters)
        //{
        //    this.coreValidator.ExactParameterLength(commandParameters, 2);

        //    this.coreValidator.EmployeeCount(this.database.Employees.Count);

        //    int employeeId = this.coreValidator.IntFromString(commandParameters[1], "employeeId");

        //    var employee = this.coreValidator.EmployeeById(this.database.Employees, employeeId);

        //    this.coreValidator.CheckNullObject(employee);

        //    if (employee.IsHired)
        //    {
        //        employee.Responsibilities.Clear();
        //        employee.IsHired = false;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Employee is already fired!");
        //    }

        //    this.writer.Write($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }





    }

}

