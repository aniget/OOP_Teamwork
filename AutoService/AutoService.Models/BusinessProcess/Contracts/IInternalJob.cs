using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IInternalJob : IJob
    {
        DepartmentType ClientDepartment { get; }

        // this will print a receipt to the department which ordered the process (the recipient of the service)
        void IssueProcessReceipt();
    }
}
