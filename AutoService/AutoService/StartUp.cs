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
    class StartUp
    {
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<AutoServiceFactory>().As<IAutoServiceFactory>().SingleInstance();
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();
            builder.RegisterType<Warehouse>().As<IWarehouse>().SingleInstance();
            builder.RegisterType<Writer>().As<IWriter>().SingleInstance();
            builder.RegisterType<Reader>().As<IReader>().SingleInstance();
            builder.RegisterType<IOWrapper>().As<IIOWrapper>().SingleInstance();


            builder.RegisterType<StockManager>().As<IStockManager>().SingleInstance();
            builder.RegisterType<ValidateCore>().As<IValidateCore>().SingleInstance();
            builder.RegisterType<ValidateModel>().As<IValidateModel>().SingleInstance();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            //Commands
            builder.RegisterType<HireEmployee>().Named<ICommand>("hireEmployee");
            builder.RegisterType<ShowEmployees>().Named<ICommand>("showEmployees");
            builder.RegisterType<ShowAllEmployeesAtDepartment> ().Named<ICommand>("showAllEmployeesAtDepartment");
            builder.RegisterType<FireEmployee> ().Named<ICommand>("fireEmployee");
            builder.RegisterType<RegisterSupplier> ().Named<ICommand>("registerSupplier");
            builder.RegisterType<RemoveSupplier> ().Named<ICommand>("removeSupplier");
            builder.RegisterType<CreateBankAccount> ().Named<ICommand>("createBankAccount");
            builder.RegisterType<DepositCashInBank> ().Named<ICommand>("depositCashInBank");
            builder.RegisterType<WithdrawCashFromBank> ().Named<ICommand>("withdrawCashFromBank");
            builder.RegisterType<IssueInvoices> ().Named<ICommand>("issueInvoices");
            builder.RegisterType<RegisterClient>().Named<ICommand>("registerClient");
            builder.RegisterType<ChangeClientName>().Named<ICommand>("changeClientName");
            builder.RegisterType<ChangeSupplierName>().Named<ICommand>("changeSupplierName");
            builder.RegisterType<OrderStockToWarehouse>().Named<ICommand>("orderStockToWarehouse");
            builder.RegisterType<RemoveClient>().Named<ICommand>("removeClient");
            builder.RegisterType<ListWarehouseItems>().Named<ICommand>("listWarehouseItems");


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
