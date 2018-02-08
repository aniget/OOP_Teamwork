using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.ValidatorTests
{
    [TestClass]
    public class ValidatorShould
    {
        [TestMethod]
        public void ThrowException_WhenCheckNullObject_IsProvidedNullValue()
        {
            var mockValidator = new Mock<IValidateCore>();
            mockValidator.Setup(r => r.CheckNullObject(It.IsAny<object>() == null)).Throws(new ArgumentException());

            Assert.ThrowsException<ArgumentException>(() => mockValidator.Object.CheckNullObject(null));
        }
        [TestMethod]
        public void ThrowException_WhenCheckNullObject_IsProvidedNullValue2()
        {
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
           // mockValidator.CheckNullObject(null);


            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(null));
        }
    }
}
