
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Assets.Contracts
{
    public interface IStock : IAsset
    {
        decimal PurchasePrice { get; }

        ICounterparty Supplier { get; }
    }
}
