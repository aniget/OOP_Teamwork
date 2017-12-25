using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IRepairAsset: IWork
    {
        IAsset Asset { get; }
        int EstimatedTimeInMinutes { get; }

        bool PurchaseRepairService { get; } //Does the repair require to call a Supplier.
        
    }
}
