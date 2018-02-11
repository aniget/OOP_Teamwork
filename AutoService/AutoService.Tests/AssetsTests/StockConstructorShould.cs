using System;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AssetsTests
{
    [TestClass]
    public class StockConstructorShould
    {
        [TestMethod]
        public void ThrowArgumentException_WhenSupplierIsNull()
        {
            //Arrange
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("stock name", stubEmployee.Object, "uniqueNumber", 15.5m, null));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEmployeeIsNull()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("stock name", null, "uniqueNumber", 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNameIsNull()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(null, stubEmployee.Object, "uniqueNumber", 15.5m, stubSupplier.Object));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenStockNameIsEmpty()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("", stubEmployee.Object, "uniqueNumber", 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNameIsWhiteSpace()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(new string(' ', 5), stubEmployee.Object, "uniqueNumber", 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNameIsLargeValue()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(new string('*', 201), stubEmployee.Object, "uniqueNumber", 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNumberIsNull()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(new string('*', 10), stubEmployee.Object, null, 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNumberIsEmpty()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(new string('*', 10), stubEmployee.Object, "", 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenStockNumberIsWhitespace()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock(new string('*', 10), stubEmployee.Object, new string(' ', 2), 15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPurchasePriceIsNegative()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("testName", stubEmployee.Object, "testNumber", -15.5m, stubSupplier.Object));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPurchasePriceIsLargerThanMillion()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("testName", stubEmployee.Object, "testNumber", 1000000+1, stubSupplier.Object));
        }

        [TestMethod]
        public void ProvideObject_WhenCorrectParametersProvided()
        {
            //Arrange
            var stubSupplier = new Mock<ICounterparty>();
            var stubEmployee = new Mock<IEmployee>();
            var stockName = "testName";
            var uniqueNumber = "1234567890";
            var price = 15.5m;
            
            //Act
            var sut = new Stock(stockName, stubEmployee.Object, uniqueNumber, price, stubSupplier.Object);

            //Assert
            Assert.IsNotNull(sut);
        }
    }
}
