using System;
using System.Collections.Generic;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.ModelsTests
{
    [TestClass]
    public class EmployeeShould
    {
        [TestMethod]
        public void Have_ListOfResponsibilityTypes_WhenItIsCreated()
        {
            //Arrange
            var stubValidator = new Mock<IValidateModel>();
            var employee = new Employee("testname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, stubValidator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(employee.Responsibilities, typeof(IList<ResponsibilityType>));
        }

        [TestMethod]
        public void CallValidatorMethodForNullOrEmpty_WhenFirstName_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.FirstName = "new name";

            //Assert
            validator.Verify(x => x.StringForNullEmpty(It.IsAny<string>()), Times.Exactly(4)); // FirstName, LastName, Position + the change of FirstName
        }

       [TestMethod]
       public void CallValidatorMethodForDigitInString_WhenFirstName_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.FirstName = "new name";
            //Assert
            validator.Verify(x => x.HasDigitInString(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3)); // FirstName, LastName + the change of FirstName
        }

        [TestMethod]
        public void CallValidatorMethodForNullOrEmpty_WhenLastName_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.LastName = "new name";

            //Assert
            validator.Verify(x => x.StringForNullEmpty(It.IsAny<string>()), Times.Exactly(4)); // FirstName, LastName, Position + the change of FirstName
        }

        [TestMethod]
        public void CallValidatorMethodForDigitInString_WheLastName_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.LastName = "new name";
            //Assert
            validator.Verify(x => x.HasDigitInString(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3)); // FirstName, LastName + the change of FirstName
        }

        [TestMethod]
        public void CallValidatorMethodForNegativeSalary_WhenSalary_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.Salary = -1000;
            validator.Verify(x => x.NonNegativeValue(It.IsAny<decimal>(), It.IsAny<string>()), Times.Exactly(3)); //salary, rpm + change of salary
        }

        [TestMethod]
        public void CallValidatorMethodForNegativeRate_WhenRatePerMinute_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.RatePerMinute = -10;

            //Assert
            validator.Verify(x => x.NonNegativeValue(It.IsAny<decimal>(), It.IsAny<string>()), Times.Exactly(3)); //salary, rpm + change of rpm
        }

        [TestMethod]
        public void ThrowArgumentException_WhenAlreadyFiredEmployee_IsFiredAgain() // :)
        {
            //Arrange
            var stubValidator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, stubValidator.Object);
            //Act
            employee.IsHired = true;

            //Assert
            Assert.ThrowsException<ArgumentException>(() => employee.IsHired = true);
        }
    }
}