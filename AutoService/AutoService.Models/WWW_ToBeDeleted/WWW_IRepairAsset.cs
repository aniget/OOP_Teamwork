using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.WWW_ToBeDeleted
{
    public interface IRepairAsset: IWork
    {
        IAsset Asset { get; }
        int EstimatedTimeInMinutes { get; }

        bool PurchaseRepairService { get; } //Does the repair require to call a Supplier.
        
    }
}
