using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Common.Enums;
using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface WWW_IOrderedService
    {
        CostServiceType CostService { get; }
        DateTime PeriodStart { get; }
        DateTime PeriodEnd { get; }
        ICollection<DepartmentType> CostServiceUsedBy { get;  }

        void AddCostServiceToDepartment();
        void RemoveCostServiceFromDepartment();
    }
}
