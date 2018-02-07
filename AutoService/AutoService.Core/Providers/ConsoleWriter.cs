using System;
using AutoService.Core.Contracts;

namespace AutoService.Core.Providers
{
    public class ConsoleWriter : IWriter //, IConsoleWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
