using Autofac;
using AutoService.Core;
using AutoService.Core.Commands;
using AutoService.Core.Commandsа;
using AutoService.Core.Contracts;
using AutoService.Core.Factory;
using AutoService.Core.Manager;
using AutoService.Core.Providers;
using AutoService.Core.Validator;
using AutoService.Models.Validator;
using System.Reflection;

namespace AutoService.AutofacConfig
{
    public class AutofacConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IEngine)))
            //    //.Where(x => x.Namespace.Contains("Factories") ||
            //    //            x.Namespace.Contains("Providers") ||
            //    //            x.Name.EndsWith("Engine"))
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<AutoServiceFactory>().As<IAutoServiceFactory>().SingleInstance();
            builder.RegisterType<Database>().As<IDatabase>().SingleInstance();

            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();

            builder.RegisterType<StockManager>().As<IStockManager>().SingleInstance();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().SingleInstance();
            builder.RegisterType<InvoiceManager>().As<IInvoiceManager>().SingleInstance();
            builder.RegisterType<BankAccountManager>().As<IBankAccountManager>().SingleInstance();
            builder.RegisterType<CounterPartyManager>().As<ICounterPartyManager>().SingleInstance();

            builder.RegisterType<ValidateCore>().As<IValidateCore>().SingleInstance();
            builder.RegisterType<ValidateModel>().As<IValidateModel>().SingleInstance();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            //Commands
            builder.RegisterType<HireEmployee>().Named<ICommand>("hireEmployee");
            builder.RegisterType<ShowEmployees>().Named<ICommand>("showEmployees");
            builder.RegisterType<ShowAllEmployeesAtDepartment>().Named<ICommand>("showAllEmployeesAtDepartment");
            builder.RegisterType<FireEmployee>().Named<ICommand>("fireEmployee");
            builder.RegisterType<RegisterSupplier>().Named<ICommand>("registerSupplier");
            builder.RegisterType<RemoveSupplier>().Named<ICommand>("removeSupplier");
            builder.RegisterType<CreateBankAccount>().Named<ICommand>("createBankAccount");
            builder.RegisterType<DepositCashInBank>().Named<ICommand>("depositCashInBank");
            builder.RegisterType<WithdrawCashFromBank>().Named<ICommand>("withdrawCashFromBank");
            builder.RegisterType<IssueInvoices>().Named<ICommand>("issueInvoices");
            builder.RegisterType<RegisterClient>().Named<ICommand>("registerClient");
            builder.RegisterType<ChangeClientName>().Named<ICommand>("changeClientName");
            builder.RegisterType<ChangeSupplierName>().Named<ICommand>("changeSupplierName");
            builder.RegisterType<OrderStockToWarehouse>().Named<ICommand>("orderStockToWarehouse");
            builder.RegisterType<RemoveClient>().Named<ICommand>("removeClient");
            builder.RegisterType<ListWarehouseItems>().Named<ICommand>("listWarehouseItems");
            builder.RegisterType<ListClients>().Named<ICommand>("listClients");
            builder.RegisterType<Help>().Named<ICommand>("help");
            builder.RegisterType<AddClientPayment>().Named<ICommand>("addClientPayment");
            builder.RegisterType<AddEmployeeResponsibility>().Named<ICommand>("addEmployeeResponsibility");
            builder.RegisterType<RemoveEmployeeResponsibility>().Named<ICommand>("removeEmployeeResponsibility");
            builder.RegisterType<SellStockToClientVehicle>().Named<ICommand>("sellStockToClientVehicle");
            builder.RegisterType<AddVehicleToClient>().Named<ICommand>("addVehicleToClient");
            builder.RegisterType<SellServiceToClientVehicle>().Named<ICommand>("sellServiceToClientVehicle");
            builder.RegisterType<ChangeEmployeeSalary>().Named<ICommand>("changeEmployeeSalary");
        }
    }
}
