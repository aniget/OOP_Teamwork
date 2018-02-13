using Autofac;
using AutoService.Core.Contracts;

namespace AutoService.Core.Providers
{
    public class ProcessorLocator : IProcessorLocator
    {

        private readonly IComponentContext container;

        public ProcessorLocator(IComponentContext container)
        {
            this.container = container;

        }

        T IProcessorLocator.GetProcessor<T>()
        {
            return container.Resolve<T>();

        }
    }
}
