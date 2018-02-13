namespace AutoService.Core.Contracts
{
    public interface IProcessorLocator
    {
        T GetProcessor<T>();
    }
}
