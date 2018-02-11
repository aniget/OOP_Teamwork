using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AutoService.Tests.ModelsTests
{
    [TestClass]
    public class EmployeeShould
    {
        [TestMethod]
        public void Have_ListOfResponsibilityTypes_WhenIsCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var employee = new Employee("testname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(employee.Responsibilities, typeof(List<ResponsibilityType>));
        }


        [TestMethod]
        public void CallValidatorMethod_WhenFirstName_IsEmpty()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            employee.FirstName = "";
            string isEmpty = "";
            //Assert
            validator.Verify(x => x.StringForNullEmpty(isEmpty), Times.AtLeastOnce);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenFirstName_HasDigits()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.FirstName = "joe1234";
            validator.Verify(x => x.HasDigitInString("joe1234", "first name"), Times.AtLeastOnce);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenLastName_IsNull()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.LastName = null;
            string isNull = null;
            //Assert
            validator.Verify(x => x.StringForNullEmpty(isNull), Times.AtLeastOnce);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenLastName_IsEmpty()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.LastName = "";
            string isEmpty = "";
            //Assert
            validator.Verify(x => x.StringForNullEmpty(isEmpty), Times.AtLeastOnce);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenLastName_HasDigits()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            validator.Setup(x => x.HasDigitInString("joe1234", "last name")).Verifiable();
            //Act
            var employee = new Employee("firstname", "joe1234", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            validator.Verify();
        }

        [TestMethod]
        public void CallValidatorMethod_WhenSalaryHasNegativeValue()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.Salary = -1000;
            validator.Verify(x => x.NonNegativeValue(-1000, "salary"), Times.AtLeastOnce);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenRatePerMinute_SetedToNegative()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.RatePerMinute = -10;

            //Assert
            validator.Verify(x => x.NonNegativeValue(-10, "rate per minute"), Times.AtLeastOnce);
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

    }
}
