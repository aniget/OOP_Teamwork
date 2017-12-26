using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellService : ISell
    {
        ISellableService Service { get; }

        DateTime StartDateTime { get; }

        bool IsFinished { get; } // let's remove
    }
}
