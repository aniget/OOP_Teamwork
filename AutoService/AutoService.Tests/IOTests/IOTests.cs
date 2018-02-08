using Autofac;
using AutoService.Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace AutoService.Tests.IOTests
{
    [TestClass]
    public class IOTests
    {
        [TestMethod]
        public void IOTest()
        {
            StreamReader input = new StreamReader("./../../../../ZeroTests/ZeroTestHQCInput.txt");
            StreamReader output = new StreamReader("./../../../../ZeroTests/ZeroTestHQCOutput.txt");
            StringWriter result = new StringWriter();
            
            Console.SetIn(input); //==console.readline
            Console.SetOut(result); //==console.writeline

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(StartUp)));

            var container = builder.Build();

            IEngine engine = container.Resolve<IEngine>();
            engine.Run();

            string expected = output.ReadToEnd().Trim();
            string actual = result.ToString().Trim();

            Assert.AreEqual(expected, actual);
        }
    }
}
