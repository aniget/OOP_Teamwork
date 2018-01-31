using System;
using AutoService.Models.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AssetsTests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void Stock_Constructor_ShouldThrowArgumentException_WhenSuppliersIsNull()
        {
            //Arrange
            FakeEmployee testEmployee = new FakeEmployee();

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Stock("stock name", testEmployee, "uniqueNumber", 15.5m, null));
        }
    }
}
