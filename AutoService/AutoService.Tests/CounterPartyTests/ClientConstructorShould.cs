using System;
using System.Collections.Generic;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.CounterPartyTests
{
    [TestClass]
    public class ClientConstructorShould
    {
        [TestMethod]
        public void ThrowArgumentException_WhenUniqueNumber_IsNull()
        {
            //Arrange
            string uniqueNumber = null;
            var validator = new Mock<IValidateModel>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", uniqueNumber, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenUniqueNumber_HasNonDigitsChars()
        {
            //Arrange
            string uniqueNumber = "test12345";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", uniqueNumber, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenUniqueNumber_IsLessThanNineDigits()
        {
            //Arrange
            string uniqueNumber = "12345678";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", uniqueNumber, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenName_IsNull()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
           //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(null, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WnenAddress_IsLessThanFiveSymbols()
        {
            //Arrange
            var addressLessThan5Symbols = new string('a', 4);
            var validator = new Mock<IValidateModel>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", addressLessThan5Symbols, "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WnenAddress_IsNull()
        {
            //Arrange
            string nullAddress = null;
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", nullAddress, "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenAddress_IsWhiteSpace()
        {
            //Arrange
            var whiteSpaceAddress = " ";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", whiteSpaceAddress, "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenName_IsWhiteSpace()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var nameIsWhiteSpace = " ";
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(nameIsWhiteSpace, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenName_IsLessThanFiveChars()
        {
            //Arrange
            var nameLessThan5Chars = new string('s', 4);
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(nameLessThan5Chars, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenDueDaysAllowed_IsNegative()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act
            validator.Setup(x => x.NonNegativeValue(-1, It.IsAny<string>())).Throws<ArgumentException>();
            //Assert
            Assert.ThrowsException<ArgumentException>(() => client.DueDaysAllowed = -1);
        }
          
        [TestMethod]
        public void CallValidationMethod_WhenDueDaysAllowed_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act
            client.DueDaysAllowed = -1;
            //Assert
            validator.Verify(x => x.NonNegativeValue(It.IsAny<decimal>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ProvideListOfVehicles_WhenIt_IsCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(client.Vehicles, typeof(List<IVehicle>));
           
        }

        [TestMethod]
        public void ProvideListOfinvoices_WhenIt_IsCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(client.Invoices, typeof(List<IInvoice>));
            
        }

        [TestMethod]
        public void ThrowArgumentException_WhenDiscountSetToNegative()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => client.Discount = -1);
        }

        [TestMethod]
        public void ThowArgumentException_WhenDiscountSetToMoreThanHundredPercent()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => client.Discount = 1.01m);
        }

        [TestMethod]
        public void ProvideObjectWithExactlyFiveDueDaysAllowed_WhenObjectCreaded()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsTrue(client.DueDaysAllowed == 5); 
        }
    }
}
