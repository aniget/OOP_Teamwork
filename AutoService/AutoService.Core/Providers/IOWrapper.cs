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
    public class IOWrapper : IIOWrapper
    {
        //Fields
        private readonly IWriter writer;
        private readonly IReader reader;
        //Constructor
        
        public IOWrapper(IWriter writer, IReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }
        //Methods
        public string ReadWithWrapper()
        {
            return this.reader.Read();
        }

        public void WriteLineWithWrapper()
        {
            this.writer.WriteLine();
        }

        public void WriteWithWrapper(object obj)
        {
            Console.Write(obj);
        }
        public void WriteLineWithWrapper(object obj)
        {
            this.writer.WriteLine(obj);
        }

        public void WriteLineWithWrapper(_Exception e)
        {
            this.writer.WriteLine(e);
        }

        public void WriteLineWithWrapper(InvalidIdException e)
        {
            this.writer.WriteLine(e);
        }

        public void WriteLineWithWrapper(ArgumentException e)
        {
            this.writer.WriteLine(e);
        }


    }
}
