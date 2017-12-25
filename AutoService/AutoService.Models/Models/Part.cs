using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using System;

namespace AutoService.Models.Models
{
    public class Part : IPart  //, IVendor
    {
        private string name;
        private string number;
        private decimal purchasePrice;
        private string oeNumbers;
        private string producer;
        private string vendor;
        private decimal mountTime; //measured in hours, estimation provided by Employee

        public Part(string name, string number, decimal purchasePrice, string oeNumbers, string producer, string vendor, PartMainCategory mainCategory, PartSubCategory subCategory)
        {
            this.Name = name;
            this.Number = number;
            this.PurchasePrice = purchasePrice;
            this.OeNumbers = oeNumbers;
            this.Producer = producer;
            this.Vendor = vendor;
            this.PartMainCategory = mainCategory;
        }

        public Part(string partName, string partNumber, decimal partPurchasePrice, string partOENumbers, string partProducer, string partVendor, PartMainCategory partMainCategory, PartSubCategory partSubCategory, decimal partMountTime)
            : this(partName, partNumber, partPurchasePrice, partOENumbers, partProducer, partVendor, partMainCategory, partSubCategory)
        {
            this.PartMountTime = partMountTime;
        }


        public PartMainCategory PartMainCategory { get; }
        public PartSubCategory PartSubCategory { get; }

        public string Name
        {
            get { return this.name; }
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
                this.name = value;
            }
        }

        public string Number
        {
            get { return this.number; }
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
                this.number = value;
            }
        }

        public decimal PurchasePrice
        {
            get { return this.purchasePrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price must be greater than 0.");
                }
                this.purchasePrice = value;
            }
        }

        public string OeNumbers
        {
            get { return this.oeNumbers; }
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
                this.oeNumbers = value;
            }
        }

        public string Producer
        {
            get { return this.producer; }
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
                this.producer = value;
            }
        }

        public string Vendor
        {
            get { return this.vendor; }
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
                this.vendor = value;
            }
        }

        public decimal PartMountTime
        {
            get { return this.mountTime; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Time to mount the part must be greater than 0.");
                }
                this.mountTime = value;
            }
        }




        //TODO: work on the methods
        public void OrderPart()
        {
            //Contact Vendor and ask for Part by providing PartOENUmber OR PartNumber 
            //Vendor confirms delivery time
            throw new NotImplementedException();
        }

        public void ReceivePart()
        {
            //Add part to the client's "ShoppingCart"
            throw new NotImplementedException();
        }

        public void PayPartToSupplier()
        {
            //Reduce our Acounts Payable with this Vendor
            throw new NotImplementedException();
        }

        public void MountPart()
        {
            //Move part from "ShoppingCart" to Employee to CAR depending on Mount Time 
            throw new NotImplementedException();
        }

        public void ReturnPartToSupplier()
        {
            //PartNumber orderd <> PartNumber Received
            //Reduce Accounts Payable
            throw new NotImplementedException();
        }

        public decimal GeneratePartSellPrice(decimal partPurchasePrice)
        {
            return partPurchasePrice * 2;
        }

    }
}
