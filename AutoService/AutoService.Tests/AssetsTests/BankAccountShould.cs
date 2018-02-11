using System;
using AutoService.Models.Assets;
using AutoService.Models.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AssetsTests
{
    [TestClass]
    public class BankAccountShould
    {
        [TestMethod]
        public void Throw_WhenBalanceProperty_IsProvidedNegativeValue()
        {
            //Arrange
            var stubEmployee = new Mock<IEmployee>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));
            
            var sut = new BankAccount("testaccount", stubEmployee.Object, "1", stubTime.Object.UtcNow);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Balance = -1);
        }

        [TestMethod]
        public void SetCorrectBalance_WhenBalanceProperty_IsProvidedCorrectValue()
        {
            //Arrange
            var stubEmployee = new Mock<IEmployee>();
            var stubTime = new Mock<TimeProvider.TimeProvider>();
            stubTime.SetupGet(tp => tp.UtcNow).Returns(new DateTime(2010, 3, 11));

            var sut = new BankAccount("testaccount", stubEmployee.Object, "1", stubTime.Object.UtcNow);

            //Act
            sut.Balance = 5000m;

            //Assert
            Assert.AreEqual(5000m, sut.Balance);
        }
    }
}
