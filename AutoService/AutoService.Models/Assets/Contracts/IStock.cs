using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IStock : IAsset
    {
        decimal PurchasePrice { get; }

        ICounterparty Vendor { get; }

        //void OrderPart();

        //void ReceivePart();

        //void PayPartToSupplier();

        //void MountPart();

        //void ReturnPartToSupplier();

        //decimal GeneratePartSellPrice(decimal partPurchasePrice);

    }
}
