﻿using Autofac;
using AutoService.Core.Contracts;

namespace AutoService.Core.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private IComponentContext container;

        public CommandFactory(IComponentContext container)
        {
            this.container = container;
        }

        public ICommand CreateCommand(string commandName)
        {
            return this.container.ResolveNamed<ICommand>(commandName);
        }
    }
}