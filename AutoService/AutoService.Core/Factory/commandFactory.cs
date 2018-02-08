using System;
using Autofac;
using Autofac.Core.Registration;
using AutoService.Core.Contracts;
using System;
using Autofac.Core.Registration;

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
            try
            {
                return this.container.ResolveNamed<ICommand>(commandName);

            }
            catch (Exception)
            {

                
            }
            
            
            
        }
    }
}