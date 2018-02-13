using Autofac.Core.Registration;
using AutoService.Core.Contracts;
using AutoService.Models.CustomExceptions;
using System;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;

        //constructor
        public Engine (IProcessorLocator processorLocator)
        {
            if (processorLocator == null) throw new ArgumentNullException();
            this.CommandFactory = processorLocator.GetProcessor<ICommandFactory>() ?? throw new ArgumentNullException();
            this.writer = processorLocator.GetProcessor<IWriter>() ?? throw new ArgumentNullException();
            this.reader = processorLocator.GetProcessor<IReader>() ?? throw new ArgumentNullException();
        }

        public ICommandFactory CommandFactory { get; }

        public void Run()
        {
            var inputLine = ReadCommand();
            var commandParameters = new string[] { string.Empty };


            while (inputLine != "exit")
            {

                commandParameters = ParseCommand(inputLine);

                try
                {
                    ICommand command = this.CommandFactory.CreateCommand(commandParameters[0]);
                    command.ExecuteThisCommand(commandParameters);
                }
                catch (NotSupportedException e) { this.writer.Write(e.Message); }
                catch (InvalidOperationException e) { this.writer.Write(e.Message); }
                catch (InvalidIdException e) { this.writer.Write(e.Message); }
                catch (ArgumentException e) { this.writer.Write(e.Message); }
                catch (ComponentNotRegisteredException) { this.writer.Write($"There is no command named [{inputLine}] implemented! Please contact Dev team to implement it :)"); }

                this.writer.Write(Environment.NewLine + "<>-<>-<>-<>-<>-<>-<>-<>---<>-<>-<>-<>-<>-<>-<>-<>" + Environment.NewLine);
                this.writer.Write("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");

                inputLine = ReadCommand();

            }
        }

        private string ReadCommand()
        {
            return this.reader.Read();
        }

        private string[] ParseCommand(string command)
        {
            return command.Split(new string[] { ";" }, StringSplitOptions.None);
        }
    }
}