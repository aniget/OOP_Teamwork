using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        public IClient Client { get; }
        public ICar Car { get; }
        public decimal Price { get; }
        public PaymentType Payment { get; }
        public CreditTerm Credit { get; }
        public void PrintInvoice()
        {
            throw new NotImplementedException();
        }

        public void PrintReceipt()
        {
            throw new NotImplementedException();
        }

        public decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute)
        {
            throw new NotImplementedException();
        }
    }
}
