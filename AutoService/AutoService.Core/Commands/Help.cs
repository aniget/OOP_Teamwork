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
            writer.Write("This is version 1.1 of the sample AutoService software written for just 5 days." + Environment.NewLine +
                              "For suggestions on improvement please send email to holySynod@bg-patriarshia.bg" +
                              Environment.NewLine +
                              "Please keep donating in order to keep us alive! We still accept BitCoin, LiteCoin, Ethereum, PitCoin , ShitCoin and any other coin! Soon we will accept Berkshire Hathaway Inc. Class A shares!");

        }
    }
}
