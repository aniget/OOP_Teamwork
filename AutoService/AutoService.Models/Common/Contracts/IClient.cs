using System.Collections.Generic;
using AutoService.Models.Vehicles.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.Common.Contracts
{
    public interface IClient : ICounterparty
    {
        int DueDaysAllowed { get; set; }
        
        decimal Discount { get; set; }

        IList<IVehicle> Vehicles { get; }
    }
}
