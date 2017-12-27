using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IClient : ICounterparty
    {
        int DueDaysAllowed { get; }
        
        decimal Discount { get; }

        void UpdateDueDays(int dueDays); //optional
    }
}
