using System;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.ModelsTests
{
    [TestClass]
    public class InvoiceShould
    {
        [TestMethod]
        public void Have_ListOfInvoicedItems_WhenItCreated()
        {
            //Arrange
            var stubValidator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();

            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, stubValidator.Object);
            //Act & Assert
            Assert.IsInstanceOfType(sut.InvoiceItems, typeof(ICollection<ISell>));
        }

        [TestMethod]
        public void InvokeValidatorMethodStringForNullEmpty_FromConstructor()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));

            //Act
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, validator.Object);
            //Assert
            validator.Verify(v => v.StringForNullEmpty(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void InvokeValidatorMethodCheckNullObject_FromConstructor()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));

            //Act
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, validator.Object);
            //Assert
            validator.Verify(v => v.CheckNullObject(It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void InvokeValidatorMethodInvoicePositiveAmount_WhenAmountChanges()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, validator.Object);

            //Act
            sut.Amount = 5;

            //Assert
            validator.Verify(v => v.InvoicePositiveAmount(It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod]
        public void InvokeValidatorMethodInvoiceOverpaid_WhenPaidAmountChanges()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, validator.Object);

            //Act
            sut.PaidAmount = 5;

            //Assert
            validator.Verify(v => v.InvoiceOverpaid(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod]
        public void ProvideValidObject_WhenProviedValidParameters()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            var stubClient = new Mock<IClient>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));

            //Act
            var sut = new Invoice("number", stubTime.Object.UtcNow, stubClient.Object, validator.Object);

            //Assert
            Assert.IsNotNull(sut);
        }
    }
}
