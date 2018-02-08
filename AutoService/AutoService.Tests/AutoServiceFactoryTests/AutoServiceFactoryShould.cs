using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Factory;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutoService.Tests.AutoServiceFactoryTests
{
    [TestClass]
    public class AutoServiceFactoryShould
    {
        [TestMethod]
        public void ReturnClientWhenItIsCreates()
        {
            //Arange
            var validator = new Mock<IValidateModel>();
            var fac = new AutoServiceFactory();
            //Act
            var client = fac.CreateClient("testname", "testadress", "123456789", validator.Object);
            //Assert
            Assert.IsNotNull(client);
        }
    }
}
