using AutoService.Models.BusinessProcess.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class ExternalBusinessJob : ExternalJob, IExternalBusinessJob
    {
        public void PrintInvoice()
        {
            throw new System.NotImplementedException();
        }
    }
}
