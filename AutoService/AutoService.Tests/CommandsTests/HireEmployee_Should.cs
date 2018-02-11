using AutoService.Core.Commandsа;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AutoService.Tests.CommandsTests
{
    [TestClass]
    public class HireEmployee_Should
    {
        [TestMethod]
        public void RecordCorrectlyThePassedValidSalaryParam_WhenExecuted()
        {
            //hireEmployee; Jo; Ma; CleanerManager; 1000; 20; Management

            //Arrange
            string cName = "hireEmployee";
            string fname = "Jo";
            string lName = "Ma";
            string pos = "MasterOfPupets";
            string sal = "15000";
            string rpm = "200";
            string dept = "Management";

            decimal expectedSalary = decimal.Parse(sal);

            string[] commandParams = { cName, fname, lName, pos, sal, rpm, dept };

            var modelValidatorStub = new Mock<IValidateModel>();

            var coreValidatorStub = new Mock<IValidateCore>();
            coreValidatorStub
                 .Setup(x => x.DecimalFromString(sal, "salary"))
                 .Returns(expectedSalary);
            coreValidatorStub
                .Setup(x => x.DecimalFromString(rpm, "ratePerMinute"))
                .Returns(decimal.Parse(rpm));

            var employeesStub = new Mock<IList<IEmployee>>();
            var employeeStub = new Mock<IEmployee>(MockBehavior.Strict);
            var employeeStubObj = employeeStub.Object;

            var autoServiceFactoryStub = new Mock<IAutoServiceFactory>(MockBehavior.Strict);
            autoServiceFactoryStub
                .Setup(x => x.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object))
                .Returns(new Employee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object));

            employeeStubObj = autoServiceFactoryStub.Object.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object);

            var databaseStub = new Mock<IDatabase>();
            databaseStub.Setup(x => x.Employees.Add(It.IsAny<IEmployee>()));

            var writerStub = new Mock<IWriter>();
            var writerStubObj = writerStub.Object;

            ICommand sut = new HireEmployee(autoServiceFactoryStub.Object, databaseStub.Object, coreValidatorStub.Object, writerStubObj, modelValidatorStub.Object);

            //Act
            sut.ExecuteThisCommand(commandParams);


            //decimal actualSalary = databaseStub.Object.Employees[0].Salary;
            decimal actualSalary = employeeStubObj.Salary;

            //Assert
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TestMethod]
        public void CallTheExecuteCommandMethod_JustOnce_WhenExecuted()
        {
            //Arrange
            string cName = "hireEmployee";
            string fname = "Jo";
            string lName = "Ma";
            string pos = "MasterOfPupets";
            string sal = "15000";
            string rpm = "200";
            string dept = "Management";

            string[] commandParams = { cName, fname, lName, pos, sal, rpm, dept };

            var modelValidatorStub = new Mock<IValidateModel>();
            modelValidatorStub.Setup(x => x.StringForNullEmpty("string"));
            modelValidatorStub.Setup(x => x.HasDigitInString("assas", "sadsada"));
            modelValidatorStub.Setup(x => x.NonNegativeValue(100m, "paramName"));

            var coreValidatorStub = new Mock<IValidateCore>();
            coreValidatorStub
                 .Setup(x => x.DecimalFromString(sal, "salary"))
                 .Returns(decimal.Parse(sal));
            coreValidatorStub
                .Setup(x => x.DecimalFromString(rpm, "ratePerMinute"))
                .Returns(decimal.Parse(rpm));

            var employeesStub = new Mock<IList<IEmployee>>();
            var employeeStub = new Mock<IEmployee>();
            var employeeStubObj = employeeStub.Object;

            var autoServiceFactoryStub = new Mock<IAutoServiceFactory>();
            autoServiceFactoryStub
                .Setup(x => x.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object))
                .Returns(new Employee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object));

            employeeStubObj = autoServiceFactoryStub.Object.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object);

            var databaseStub = new Mock<IDatabase>();
            databaseStub.Setup(x => x.Employees.Add(It.IsAny<IEmployee>()));

            var writerStub = new Mock<IWriter>();
            var writerStubObj = writerStub.Object;

            ICommand sut = new HireEmployee(autoServiceFactoryStub.Object, databaseStub.Object, coreValidatorStub.Object, writerStubObj, modelValidatorStub.Object);

            //Act
            sut.ExecuteThisCommand(commandParams);

            //assert
            databaseStub.Verify(x => x.Employees.Add(It.IsAny<IEmployee>()), Times.Once);

        }

    }
}
