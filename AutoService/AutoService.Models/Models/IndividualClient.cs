using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public class IndividualClient : Client, IIndividual
    {
        public ICollection<IExternalIndividualsJob> ServicesProvided { get; }
    }
}
