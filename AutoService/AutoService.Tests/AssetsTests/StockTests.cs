using System;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AssetsTests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void Stock_Constructor_ShouldThrowArgumentException_WhenSupplierIsNull()
        {
            //Arrange
            var fakeEmployee = new Mock<IEmployee>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("stock name", fakeEmployee.Object, "uniqueNumber", 15.5m, null));
        }

        [TestMethod]
        public void Stock_Constructor_ShouldThrowArgumentException_WhenEmployeeIsNull()
        {
            //Arrange
            var fakeSupplier = new Mock<ICounterparty>();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("stock name", null, "uniqueNumber", 15.5m, fakeSupplier.Object));
        }
    }
}
