using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.BusinessProcess.Models
{
    public class SellPart : Sell, ISellStock
    {
        public IStock Part { get; }
        public IStock Stock { get; }
    }
}
