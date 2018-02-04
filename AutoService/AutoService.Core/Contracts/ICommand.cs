using System.Collections.Generic;

namespace AutoService.Core.Contracts
{
    public interface ICommand
    {
        void ExecuteThisCommand(string[] commandParameters);
    }
}