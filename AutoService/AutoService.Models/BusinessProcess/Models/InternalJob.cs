using System;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Models;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class InternalJob : Job, IInternalJob
    {
        public DepartmentType ClientDepartment { get; }
        public void IssueProcessReceipt()
        {
            throw new NotImplementedException();
        }
    }
}
