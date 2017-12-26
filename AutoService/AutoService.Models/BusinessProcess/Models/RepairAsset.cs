using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    public class RepairAsset: Work, WWW_IRepairAsset
    {
        public IAsset Asset { get; }
        public int EstimatedTimeInMinutes { get; }
        public bool PurchaseRepairService { get; }
    }
}
