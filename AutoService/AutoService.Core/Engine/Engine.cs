using AutoService.Models.CustomExceptions;
using System;
using Autofac.Core.Registration;
using AutoService.Core.Contracts;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;

        //constructor
        public Engine
            (
            ICommandFactory commandFactory,
            IWriter writer,
            IReader reader
            )
        {
            this.CommandFactory = commandFactory ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.reader = reader ?? throw new ArgumentNullException();
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