using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Contracts
{
    public interface IInvoiceManager
    {
        void SetInvoice(IInvoice invoice);

        void IncreasePaidAmount(decimal amount);

        void CalculateInvoiceAmount();
    }
}
