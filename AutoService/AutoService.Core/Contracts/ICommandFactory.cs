namespace AutoService.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName);

    }
}