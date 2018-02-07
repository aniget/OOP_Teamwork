using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;

namespace AutoService.Core.Providers
{
    public class ConsoleReader : IConsoleReader, IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
