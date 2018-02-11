using Autofac;
using AutoService.Core.Contracts;
using System;

namespace AutoService.Core.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private IComponentContext container;

        public CommandFactory(IComponentContext container)
        {
            this.container = container ?? throw new ArgumentNullException();
        }

        public ICommand CreateCommand(string commandName)
        {
            return this.container.ResolveNamed<ICommand>(commandName);
        }
    }
}