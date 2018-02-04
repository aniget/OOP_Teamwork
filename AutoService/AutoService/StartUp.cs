using Autofac;
using AutoService.Core;
using AutoService.Core.Commands;
using AutoService.Core.Contracts;
using AutoService.Core.Factory;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using System.Threading.Tasks;

namespace AutoService
{
    class StartUp
    {
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<AutoServiceFactory>().As<IAutoServiceFactory>();
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            builder.RegisterType<Warehouse>().AsSelf().SingleInstance();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            
            builder.RegisterType<HireEmployeeCommand>().Named<ICommand>("hireEmployee");
            builder.RegisterType<ShowEmployeesCommand>().Named<ICommand>("showEmployees");
            
            //the other commands will follow below

            //IContainer containerToRegister = null;
            //builder.Register(c => containerToRegister);
            //builder.RegisterBuildCallback(c => containerToRegister = c);

            var container = builder.Build();


            var engine = container.Resolve<IEngine>();
            
            engine.Run();
        }
    }
}
