namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface IExternalBusinessJob : IExternalJob
    {
        void PrintInvoice();
    }
}
