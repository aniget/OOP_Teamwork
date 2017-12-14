namespace AutoService.Models.Contracts
{
    public interface IClient : ICompany
    {
        int DueDaysAllowed { get; }

        void UpdateDueDays(int dueDays);

        void SendReminderForOutstandingInvoice();
    }
}
