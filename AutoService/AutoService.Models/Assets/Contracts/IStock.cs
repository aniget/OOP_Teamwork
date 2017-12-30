using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets.Contracts
{
    public interface IStock : IAsset/*, ISellStock*/
    {
        decimal PurchasePrice { get; }

        ICounterparty Supplier { get; }

    

        //void OrderPart();

        //void ReceivePart();

        //void PayPartToSupplier();

        //void MountPart();

        //void ReturnPartToSupplier();

        //decimal GeneratePartSellPrice(decimal partPurchasePrice);

    }
}
