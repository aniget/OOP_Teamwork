using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;

namespace AutoService.Models.Models
{
    public class OrderedService : IOrderedService
    {
        public CostServiceType CostService { get; }
        public DateTime PeriodStart { get; }
        public DateTime PeriodEnd { get; }
        public ICollection<DepartmentType> CostServiceUsedBy { get; }
        public void AddCostServiceToDepartment()
        {
            throw new NotImplementedException();
        }

        public void RemoveCostServiceFromDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
