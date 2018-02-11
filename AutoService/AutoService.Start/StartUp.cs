using System.Reflection;
using Autofac;
using AutoService.Core.Contracts;
using AutoService.Core.Manager;

namespace AutoService
{
    public class StartUp
    {
        public static void Main()
        {
            //    builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfig.AutofacConfig());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
