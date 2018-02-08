using System;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.ValidatorTests
{
    [TestClass]
    public class ValidatorShould
    {
        [TestMethod]
        public void ThrowException_WhenCheckNullObjectMethod_IsProvidedNullValue()
        {
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            
            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(null));
        }
        [TestMethod]
        public void ThrowException_WhenCheckNullObjectMethod_IsProvidedStringOfNullValues()
        {
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            var nullList = new object[] { null, null };

            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(nullList));
            
        }
    }
}
