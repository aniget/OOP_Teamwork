using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.ClientShould
{
    [TestClass]
    public class ClientShould
    {
        [TestMethod]
        public void ThrowArgumetException_WnenQuniqueNumber_IsNull()
        {
            //Arrange
            string num = null;
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", num, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenQuniqueNumber_HasNonDigitsChars()
        {
            //Arrange
            string num = "test12345";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", num, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenQuniqueNumber_IsLessThanNineDigits()
        {
            //Arrange
            string num = "12345678";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", "testadress", num, validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenName_IsNull()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
           //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(null, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenAdress_IsNull()
        {
            //Arrange
            var adressLessThan5Symbols = "Moon";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", adressLessThan5Symbols, "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenAdress_IsWhiteSpace()
        {
            //Arrange
            var adressIsWhiteSpace = " ";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client("testname", adressIsWhiteSpace, "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenName_IsWhiteSpace()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var nameIsWhiteSpace = " ";
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(nameIsWhiteSpace, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumetException_WnenName_IsLessThanFiveChars()
        {
            //Arrange
            var nameLessThan5Chars = "Jo";
            var validator = new Mock<IValidateModel>();
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Client(nameLessThan5Chars, "testadress", "123456789", validator.Object));
        }

        [TestMethod]
        public void ThrowArgumnetEcxeption_WhenDueDaysAllowed_IsNegative()
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
        public void CallValidationMethod_WhenDueDaysAllowed_IsSetedToNegative()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act
            client.DueDaysAllowed = -1;
            //Assert
            validator.Verify(x => x.NonNegativeValue(-1, "due days allowed"), Times.Exactly(1));
        }

        [TestMethod]
        public void HaveListOfVehicles_WhenIt_IsCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(client.Vehicles, typeof(List<IVehicle>));
           
        }

        [TestMethod]
        public void HaveListOfinvoices_WhenIt_IsCreated()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(client.Invoices, typeof(List<IInvoice>));
            
        }

        [TestMethod]
        public void ThowArgumnetException_WhenDiscountSetedToNegative()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => client.Discount = -1);
        }

        [TestMethod]
        public void ThowArgumnetException_WhenDiscountSetedToMoreThanHundredPercents()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => client.Discount = 1.01m);
        }

        [TestMethod]
        public void HaveExactlyFiveDueDaysAllowedWhenItCreaded()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();

            var client = new Client("testname", "testadress", "123456789", validator.Object);
            //Act & Assert
            Assert.IsTrue(client.DueDaysAllowed == 5); 
        }
    }
}
