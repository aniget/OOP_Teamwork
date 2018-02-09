using System.Reflection;
using Autofac;
using AutoService.Core.Contracts;



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
