using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.CustomExceptions;

namespace AutoService.Core.Contracts
{
    public interface IIOWrapper
    {
        void WriteLineWithWrapper();

        void WriteWithWrapper(object obj);

        void WriteLineWithWrapper(object obj);

        void WriteLineWithWrapper(_Exception e);

        void WriteLineWithWrapper(InvalidIdException e);

        void WriteLineWithWrapper(ArgumentException e);

        string ReadWithWrapper();
    }
}
