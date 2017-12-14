using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using System;

namespace AutoService.Models.Models
{
    class Part : IPart  //, IVendor
    {
        private string partName;
        private string partNumber;
        private decimal partPurchasePrice;
        private string partOENumbers;
        private string partProducer;
        private string partVendor;
        private decimal partMountTime; //measured in hours, estimation provided by Employee

        public Part(string partName, string partNumber, decimal partPurchasePrice, string partOENumbers, string partProducer, string partVendor, PartMainCategory partMainCategory, PartSubCategory partSubCategory)
        {
            this.PartName = partName;
            this.PartNumber = partNumber;
            this.PartPurchasePrice = partPurchasePrice;
            this.PartOENumbers = partOENumbers;
            this.PartProducer = partProducer;
            this.PartVendor = partVendor;
            this.PartMainCategory = partMainCategory;
        }

        public Part(string partName, string partNumber, decimal partPurchasePrice, string partOENumbers, string partProducer, string partVendor, PartMainCategory partMainCategory, PartSubCategory partSubCategory, decimal partMountTime)
            : this(partName, partNumber, partPurchasePrice, partOENumbers, partProducer, partVendor, partMainCategory, partSubCategory)
        {
            this.PartMountTime = partMountTime;
        }


        public PartMainCategory PartMainCategory { get; }
        public PartSubCategory PartSubCategory { get; }

        public string PartName
        {
            get { return this.partName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Part Name should not be empty");
                }
                if (value.Length > 500)
                {
                    throw new ArgumentException("Max Length 500 symbols.");
                }
                this.partName = value;
            }
        }

        public string PartNumber
        {
            get { return this.partNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Part Number should not be empty");
                }
                if (value.Length > 100)
                {
                    throw new ArgumentException("Max Length 100 symbols.");
                }
                this.partNumber = value;
            }
        }

        public decimal PartPurchasePrice
        {
            get { return this.partPurchasePrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price must be greater than 0.");
                }
                this.partPurchasePrice = value;
            }
        }

        public string PartOENumbers
        {
            get { return this.partOENumbers; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Part OE Number should not be empty");
                }
                if (value.Length > 500)
                {
                    throw new ArgumentException("Max Length 500 symbols.");
                }
                this.partOENumbers = value;
            }
        }

        public string PartProducer
        {
            get { return this.partProducer; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Part Producer should not be empty");
                }
                if (value.Length > 200)
                {
                    throw new ArgumentException("Max Length 200 symbols.");
                }
                this.partProducer = value;
            }
        }

        public string PartVendor
        {
            get { return this.partVendor; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Part Vendor should not be empty");
                }
                if (value.Length > 200)
                {
                    throw new ArgumentException("Max Length 200 symbols.");
                }
                this.partVendor = value;
            }
        }

        public decimal PartMountTime
        {
            get { return this.partMountTime; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Time to mount the part must be greater than 0.");
                }
                this.partMountTime = value;
            }
        }




        //TODO: work on the methods
        public void OrderPart()
        {
            //Contact Vendor and ask for Part by providing PartOENUmber OR PartNumber 
            //Vendor confirms delivery time
        }

        public void ReceivePart()
        {
            //Add part to the client's "ShoppingCart"
        }

        public void PayPart()
        {
            //Reduce our Acounts Payable with this Vendor
        }

        public void MountPart()
        {
            //Move part from "ShoppingCart" to Employee to CAR depending on Mount Time 
        }

        public void ReturnPartToSupplier()
        {
            //PartNumber orderd <> PartNumber Received
            //Reduce Accounts Payable
        }

        public decimal GeneratePartSellPrice(decimal partPurchasePrice)
        {
            return partPurchasePrice * 2;
        }

    }
}
