using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;
using AutoService.Models.CustomExceptions;

namespace AutoService.Core.Providers
{
    public class ConsoleWriter : IConsoleWriter, IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
