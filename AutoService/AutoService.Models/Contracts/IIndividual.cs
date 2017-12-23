using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Contracts
{
    public interface IIndividual : IClient
    {
        ICollection<IExternalIndividualsJob> ServicesProvided { get; }
    }
}
