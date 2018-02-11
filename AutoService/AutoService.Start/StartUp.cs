using Autofac;
using AutoService.Core.Contracts;

namespace AutoService
{
    public class StartUp
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacConfig.AutofacConfig());
            var container = builder.Build();

            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
