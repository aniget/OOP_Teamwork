using System;
using AutoService.Core.Manager;
using AutoService.Models.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace AutoService.Tests.EmployeeManagerTests
{
    [TestClass]
    public class EmployeeManagerShould
    {
        [TestMethod]
        public void ChangeSalary_WhenValidValueIsProvided()
        {
            //Arrange
            var fakeEmployee = new Mock<IEmployee>();
            var employeeManager = new EmployeeManager();
            fakeEmployee.Setup(s => s.Salary).Returns(100m);
            employeeManager.SetEmployee(fakeEmployee.Object);

            //Assert
            Assert.AreEqual(fakeEmployee.Object, employeeManager.Employee);
        }

        [TestMethod]
        public void CallChangeSalaryMethod_WithValidValues()
        {
            //Arrange
            var fakeEmployee = new Mock<IEmployee>();
            var employeeManager = new EmployeeManager();
            employeeManager.SetEmployee(fakeEmployee.Object);

            //Act
            employeeManager.ChangeSalary(200m);

            //Assert
            fakeEmployee.VerifySet(x => x.Salary = 200m, Times.Once());
        }

        [TestMethod]
        public void FireEmployee_WhenEmployeeIsNotFired()
        {
            //Arrange
            var fakeEmployee = new Mock<IEmployee>();
            var employeeManager = new EmployeeManager();
            employeeManager.SetEmployee(fakeEmployee.Object);

            //Act
            employeeManager.FireEmployee();

            //Assert
            fakeEmployee.VerifySet(x => x.Salary = 200m, Times.Once());
        }
    }
}
