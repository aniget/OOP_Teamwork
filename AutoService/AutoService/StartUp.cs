using Autofac;
using AutoService.Core;
using AutoService.Core.Commands;
using AutoService.Core.Contracts;
using AutoService.Core.Factory;
using AutoService.Core.Manager;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Common.Models;
using AutoService.Models.Validator;


namespace AutoService
{
    class StartUp
    {
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<AutoServiceFactory>().As<IAutoServiceFactory>().SingleInstance();
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            builder.RegisterType<Warehouse>().As<IWarehouse>().SingleInstance();
            
            builder.RegisterType<StockManager>().As<IStockManager>().SingleInstance();
            builder.RegisterType<ValidateCore>().As<IValidateCore>().SingleInstance();
            builder.RegisterType<ValidateModel>().As<IValidateModel>().SingleInstance();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();

            builder.RegisterType<HireEmployee>().Named<ICommand>("hireEmployee");
            builder.RegisterType<ShowEmployees>().Named<ICommand>("showEmployees");
            builder.RegisterType<ShowAllEmployeesAtDepartment> ().Named<ICommand>("showAllEmployeesAtDepartment");
            builder.RegisterType<FireEmployee> ().Named<ICommand>("fireEmployee");

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
