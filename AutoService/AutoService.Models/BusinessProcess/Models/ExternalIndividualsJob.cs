using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    class ExternalIndividualsJob : ExternalJob, IExternalIndividualsJob
    {
        public void PrintReceipt()
        {
            throw new System.NotImplementedException();
        }
    }
}
