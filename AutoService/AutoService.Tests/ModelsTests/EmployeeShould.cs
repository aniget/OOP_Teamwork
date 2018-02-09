using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Have_ListOfResponsibilityTypes_WhenItCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var employee = new Employee("testname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(employee.Responsibilities, typeof(List<ResponsibilityType>));
        }

        [TestMethod]
        public void CallValidatorMethod_WhenFirstName_IsNull()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            
            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            employee.FirstName = null;
            string isNull = null;
            //Assert
            validator.Verify(x => x.StringForNullEmpty(isNull), Times.Once);
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
            validator.Verify(x => x.StringForNullEmpty(isEmpty), Times.Once);
        }

        [TestMethod]
        public void CallValidatorMethod_WhenFirstName_HasDigits()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.FirstName = "joe1234";
            validator.Verify(x => x.HasDigitInString("joe1234", "first name"), Times.Once);
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
            validator.Verify(x => x.StringForNullEmpty(isNull), Times.Once);
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

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.LastName = "joe1234";
            validator.Verify(x => x.HasDigitInString("joe1234", "last name"), Times.AtLeastOnce);
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
            //Assert
            employee.RatePerMinute = -10;
            validator.Verify(x => x.NonNegativeValue(-10, "rate per minute"), Times.AtLeastOnce);
        }
        //[TestMethod]
        //public void ThrowArgumnetEcxeption_WhenDueDaysAllowed_IsNegative()
        //{
        //    //Arrange
        //    var validator = new Mock<IValidateModel>();

        //    var client = new Client("testname", "testadress", "123456789", validator.Object);
        //    //Act
        //    validator.Setup(x => x.NonNegativeValue(-1, It.IsAny<string>())).Throws<ArgumentException>();
        //    //Assert
        //    Assert.ThrowsException<ArgumentException>(() => client.DueDaysAllowed = -1);
        //}
    }
}
