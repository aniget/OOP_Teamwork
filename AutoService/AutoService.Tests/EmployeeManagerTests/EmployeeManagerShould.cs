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

            //Act
            employeeManager.ChangeSalary(200m);

            //Assert
            Assert.AreEqual(200m, 200m);
        }

        [TestMethod]
        public void ThrowException_WhenInvalidValueIsProvided()
        {
            //Arrange
            var fakeEmployee = new Mock<IEmployee>();
            var employeeManager = new EmployeeManager();
            employeeManager.SetEmployee(fakeEmployee.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => employeeManager.ChangeSalary(-200m));
        }
    }
}
