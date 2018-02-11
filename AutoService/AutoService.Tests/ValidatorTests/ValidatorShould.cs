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
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(null));
        }

        [TestMethod]
        public void ThrowException_WhenCheckNullObjectMethod_IsProvidedStringOfNullValues()
        {       
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            var listWithNullValue = new object[] { "test", null };
            
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.CheckNullObject(listWithNullValue));
        }

        [TestMethod]
        public void ThrowException_WhenIntFromStringMethod_IsProvidedString()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.IntFromString("alabala", "parameter"));
        }

        [TestMethod]
        public void ThrowException_WhenIntFromStringMethod_IsProvidedLongNumber()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.IntFromString("736283763187463284", "parameter"));
        }

        [TestMethod]
        public void ParsesCorrectValue_WhenIntFromStringMethod_IsProvidedValidIntegerNumber()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            int expectedNumber = 7384;
            
            //Act & Assert
            Assert.AreEqual(expectedNumber, mockValidator.IntFromString(expectedNumber.ToString(), "parameter"));
        }

        [TestMethod]
        public void ThrowException_WhenDecimalFromStringMethod_IsProvidedInvalidDecimalNumber()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.DecimalFromString("7362837631885984375874353457463284", "parameter"));
        }

        [TestMethod]
        public void ParsesCorrectValue_WhenDecimalFromStringMethod_IsProvidedValidDecimalNumber()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            decimal expectedNumber = 73811430193474;

            //Act & Assert
            Assert.AreEqual(expectedNumber, mockValidator.DecimalFromString("73811430193474", "parameter"));
        }

        [TestMethod]
        public void ThrowException_WhenStringForNullEmptyMethod_IsProvidedNullValue()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.StringForNullEmpty(null));
        }

        [TestMethod]
        public void ThrowException_WhenStringForNullEmptyMethod_IsProvidedStringOfNullValue()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            var listWithNullValue = new string[] { "test", null };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.StringForNullEmpty(listWithNullValue));
        }

        [TestMethod]
        public void ThrowException_WhenStringForNullEmptyMethod_IsProvidedStringOfEmptyValue()
        {
            //Arrange
            var fakeWriter = new Mock<IWriter>();
            var mockValidator = new ValidateCore(fakeWriter.Object);
            var listWithEmptyValue = new string[] { "", "test"};

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => mockValidator.StringForNullEmpty(listWithEmptyValue));
        }
    }
}