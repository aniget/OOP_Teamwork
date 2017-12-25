using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IPart
    {
        string Name { get; }

        //type is string because some numbers may start with 0
        string Number { get; }

        decimal PurchasePrice { get; }

        //comma separated
        string OeNumbers { get; }

        string Producer { get; }

        string Vendor { get; }

        //below two are not part of the Interface because they are decided when parts are put in warehouse, not when they are "interfaced" between Vendor and AutoService
        //PartMainCategory partMainCategory { get; }
        //PartSubCategory partSubCategory { get; }

        void OrderPart();

        void ReceivePart();

        void PayPartToSupplier();

        void MountPart();

        void ReturnPartToSupplier();

        decimal GeneratePartSellPrice(decimal partPurchasePrice);

    }
}
