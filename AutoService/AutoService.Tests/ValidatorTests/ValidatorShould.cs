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
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);

            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(null));
        }
    }
}
