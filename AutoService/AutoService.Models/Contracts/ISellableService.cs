using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Contracts
{
    public interface ISellableService
    {
        string Name { get; }

        int EstimatedTimeInMinutes { get; }


        
    }
}
