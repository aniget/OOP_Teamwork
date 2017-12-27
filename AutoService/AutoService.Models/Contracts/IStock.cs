using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IStock : IAsset
    {
        //type is string because some numbers may start with 0
        string Number { get; }

        decimal PurchasePrice { get; }

        //comma separated
        string OeNumbers { get; }

        string Producer { get; }

        ISupplier Vendor { get; }

        //void OrderPart();

        //void ReceivePart();

        //void PayPartToSupplier();

        //void MountPart();

        //void ReturnPartToSupplier();

        //decimal GeneratePartSellPrice(decimal partPurchasePrice);

    }
}
