using System;
using AutoService.Core.Contracts;

namespace AutoService.Core.Providers
{
    public class ConsoleReader : IReader //,IConsoleReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
