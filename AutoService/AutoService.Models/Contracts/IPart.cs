using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IPart
    {
        string PartName { get; set; }

        //type string because some numbers may start with 0
        string PartNumber { get; set; }

        decimal PartPurchasePrice { get; set; }

        //comma separated
        string PartOENumbers { get; set; }

        string PartProducer { get; set; }

        string PartVendor { get; set; }

        //below two are not part of the Interface because they are decided when parts are put in warehouse, not when they are "interfaced" between Vendor and AutoService
        //PartMainCategory partMainCategory { get; }
        //PartSubCategory partSubCategory { get; }


    }
}
