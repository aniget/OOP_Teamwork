using AutoService.Models.Enums;

namespace AutoService.Models.Contracts
{
    public interface IInvoice
    {
        string Number { get; }
        decimal Amount { get; }
        PaymentType PaymentType { get; }
    }
}
