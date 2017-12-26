using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        IClient Client { get; }

        ICar Car { get; }

        decimal Price { get; }

        PaymentType Payment { get; }

        CreditTerm Credit { get; }

        //decimal PricePerMinute { get; } //PricePerMinute will come from Employee/s assigned on the job

        void PrintInvoice();

        void PrintReceipt();

        decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute);

    }
}
