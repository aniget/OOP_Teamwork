using System.Collections;
using System.Collections.Generic;
using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.Contracts
{
    public interface IClient
    {
        int DueDaysAllowed { get; }

        void UpdateDueDays(int dueDays);
    }
}
