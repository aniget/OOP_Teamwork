using System.Collections.Generic;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Models.Common.Contracts
{
    public interface IWarehouse
    {
        IList<IStock> AvailableStocks { get; }
    }
}