using System.Reflection;
using Autofac;
using AutoService.Core;
using AutoService.Core.Commands;
using AutoService.Core.Commandsа;
using AutoService.Core.Contracts;
using AutoService.Core.Factory;
using AutoService.Core.Manager;
using AutoService.Core.Providers;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;


namespace AutoService
{
    public class StartUp
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            //the other commands will follow below

            //IContainer containerToRegister = null
            //builder.Register(c => containerToRegister);
            //builder.RegisterBuildCallback(c => containerToRegister = c);

            var container = builder.Build();

            var engine = container.Resolve<IEngine>();

            engine.Run();
        }
    }
}
