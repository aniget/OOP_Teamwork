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
    public class Writer : IWriter
    {
        public void Write(object obj)
        {
            Console.Write(obj);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }
       
        public void WriteLine(_Exception e)
        {
            Console.WriteLine(e);
        }

        public void WriteLine(InvalidIdException e)
        {
            Console.WriteLine(e);
        }

        public void WriteLine(ArgumentException e)
        {
            Console.WriteLine(e);
        }
    }
}
