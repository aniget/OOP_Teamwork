using System.Collections.Generic;
using AutoService.Models.Assets.Contracts;

namespace AutoService.Models.Common.Contracts
{
    public interface IWarehouse
    {
        List<IStock> AvailableStocks { get; }
    }
}