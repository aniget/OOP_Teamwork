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
            var employeesStub = new Mock<IList<IEmployee>>();
            var employeeStub = new Mock<IEmployee>(MockBehavior.Strict);
            var autoServiceFactoryStub = new Mock<IAutoServiceFactory>(MockBehavior.Strict);
            var databaseStub = new Mock<IDatabase>();
            var writerStub = new Mock<IWriter>();

            var processorLocatorStub = new Mock<IProcessorLocator>();
            processorLocatorStub.Setup(x => x.GetProcessor<IValidateModel>()).Returns(modelValidatorStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IValidateCore>()).Returns(coreValidatorStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IAutoServiceFactory>()).Returns(autoServiceFactoryStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IEmployee>()).Returns(employeeStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IDatabase>()).Returns(databaseStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IWriter>()).Returns(writerStub.Object);
            
            coreValidatorStub
                 .Setup(x => x.DecimalFromString(sal, "salary"))
                 .Returns(expectedSalary);
            coreValidatorStub
                .Setup(x => x.DecimalFromString(rpm, "ratePerMinute"))
                .Returns(decimal.Parse(rpm));

                        var employeeStubObj = employeeStub.Object;

            autoServiceFactoryStub
                .Setup(x => x.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object))
                .Returns(new Employee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object));

            employeeStubObj = autoServiceFactoryStub.Object.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object);

            databaseStub.Setup(x => x.Employees.Add(It.IsAny<IEmployee>()));

            var writerStubObj = writerStub.Object;

            ICommand sut = new HireEmployee(processorLocatorStub.Object);

            //Act
            sut.ExecuteThisCommand(commandParams);


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
            var coreValidatorStub = new Mock<IValidateCore>();
            var employeesStub = new Mock<IList<IEmployee>>();
            var employeeStub = new Mock<IEmployee>();
            var databaseStub = new Mock<IDatabase>();
            var writerStub = new Mock<IWriter>();
            var autoServiceFactoryStub = new Mock<IAutoServiceFactory>();

            var processorLocatorStub = new Mock<IProcessorLocator>();
            processorLocatorStub.Setup(x => x.GetProcessor<IValidateModel>()).Returns(modelValidatorStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IValidateCore>()).Returns(coreValidatorStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IAutoServiceFactory>()).Returns(autoServiceFactoryStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IEmployee>()).Returns(employeeStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IDatabase>()).Returns(databaseStub.Object);
            processorLocatorStub.Setup(x => x.GetProcessor<IWriter>()).Returns(writerStub.Object);

            modelValidatorStub.Setup(x => x.StringForNullEmpty("string"));
            modelValidatorStub.Setup(x => x.HasDigitInString("assas", "sadsada"));
            modelValidatorStub.Setup(x => x.NonNegativeValue(100m, "paramName"));

            coreValidatorStub
                 .Setup(x => x.DecimalFromString(sal, "salary"))
                 .Returns(decimal.Parse(sal));
            coreValidatorStub
                .Setup(x => x.DecimalFromString(rpm, "ratePerMinute"))
                .Returns(decimal.Parse(rpm));

            var employeeStubObj = employeeStub.Object;

            autoServiceFactoryStub
                .Setup(x => x.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object))
                .Returns(new Employee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object));

            employeeStubObj = autoServiceFactoryStub.Object.CreateEmployee(fname, lName, pos, decimal.Parse(sal), decimal.Parse(rpm), DepartmentType.Management, modelValidatorStub.Object);

            databaseStub.Setup(x => x.Employees.Add(It.IsAny<IEmployee>()));

            var writerStubObj = writerStub.Object;

            ICommand sut = new HireEmployee(processorLocatorStub.Object);

            //Act
            sut.ExecuteThisCommand(commandParams);

            //assert
            databaseStub.Verify(x => x.Employees.Add(It.IsAny<IEmployee>()), Times.Once);

        }

    }
}
