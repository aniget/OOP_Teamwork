namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellService : ISell
    {
        string ServiceName { get; }

        int DurationInMinutes { get; }
    }
}
