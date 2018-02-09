using System;
using AutoService.Core.Factory;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AutoServiceFactoryTests
{
    [TestClass]
    public class AutoServiceFactoryShould
    {
        [TestMethod]
        public void ReturnValidClient_WhenItIsProvidedValidArguments()
        {
            //Arange
            var validator = new Mock<IValidateModel>();
            var fac = new AutoServiceFactory();
            //Act
            var client = fac.CreateClient("testname", "testadress", "123456789", validator.Object);
            //Assert
            Assert.IsNotNull(client);
        }
        [TestMethod]
        public void ReturnSupplierWhenItIsCreated()
        {
            //Arange

            var fac = new AutoServiceFactory();
            //Act
            var suplier = fac.CreateSupplier("testname", "testadress", "123456789", true);
            //Assert
            Assert.IsNotNull(suplier);
        }
        [TestMethod]
        public void ReturnEmployee_WhenItIsCreated()
        {
            //Arange
            var validator = new Mock<IValidateModel>();
            
            var fac = new AutoServiceFactory();
            //Act
            var employee = fac.CreateEmployee
                (
                "testfirstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            Assert.IsNotNull(employee);
        }
        [TestMethod]
        public void ReturnInvoice_WhenItIsCreated()
        {
            //Arange
            var validator = new Mock<IValidateModel>();
            var client = new Mock<IClient>();
            var fac = new AutoServiceFactory();
            DateTime date = new DateTime();
            //Act
            var invoice = fac.CreateInvoice("1234", date, client.Object, validator.Object);
                
                
            //Assert
            Assert.IsNotNull(invoice);
        }
    } 
}
