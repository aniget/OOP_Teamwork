using System;
using AutoService.Core.Contracts;

namespace AutoService.Core.Commands
{
    public class Help : ICommand
    {
        private readonly IWriter writer;

        public Help(IWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException();
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            writer.Write("This is a sample AutoService software written for just 5 days." + Environment.NewLine +
                              "For suggestions on improvement please send email to holySynod@bg-patriarshia.bg" +
                              Environment.NewLine +
                              "Please donate in order to keep us alive! We accept BitCoin, LiteCoin, Ethereum, PitCoin , ShitCoin and any other coin!");

        }
    }
}
