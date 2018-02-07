using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.CustomExceptions;

namespace AutoService.Core.Contracts
{
    public interface IWriter
    {
        void WriteLine();

        void Write(object obj);

        void WriteLine(object msg);

        void WriteLine(_Exception e);

        void WriteLine(InvalidIdException e);

        void WriteLine(ArgumentException e);



    }
}
