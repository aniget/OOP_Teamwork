using System;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets.Contracts
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
