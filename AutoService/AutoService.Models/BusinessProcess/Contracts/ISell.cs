using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISell : IWork
    {
        IClient Client { get; }

        Vehicle Vehicle { get; }

        //decimal Price { get; }

        //PaymentType Payment { get; } // make it simple - let's delete

        //CreditTerm Credit { get; } // make it simple - let's delete

        //decimal PricePerMinute { get; } //PricePerMinute will come from Employee/s assigned on the job

        //void PrintInvoice();

        //void PrintReceipt();

        decimal CalculateRevenue(int requiredTimeInMinutes, decimal pricePerMinute);

    }
}
