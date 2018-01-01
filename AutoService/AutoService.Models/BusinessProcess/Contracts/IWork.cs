using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IWork
    {
        //even when job is performed by external party there is someone from the AutoService who is responsible for the completion
        IEmployee ResponsibleEmployee { get; }

        //both Sell and Order works have a price BUT SellService doesn't, so in case we sell Service override will follow
        //TODO: want to remove the price from ISell because we already have price in IStock and a calculatable price in SellService
        //decimal Price { get; } //commented because price comes from stock 

        //Set whether the work is Sell (revenue generating) or Order (cost generating) type of work
        TypeOfWork Job { get; }

        //decimal GetEmployeeRatePerMinute(IEmployee employee);

    }
}
