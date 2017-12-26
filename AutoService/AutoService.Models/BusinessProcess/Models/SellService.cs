using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    class SellService : Sell, ISellService
    {
        public ISellableService Service { get; }
        public string Name { get; }
        public int DurationInMinutes { get; }
    }
}
