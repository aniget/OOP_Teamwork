using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;
using AutoService.Core.Validator;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Commands
{
    public class Help : ICommand
    {
        private readonly IConsoleWriter consoleWriter;

        public Help(IConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            consoleWriter.Write("This is a sample AutoService software written for just 5 days." + Environment.NewLine +
                              "For suggestions on improvement please send email to holySynod@bg-patriarshia.bg" +
                              Environment.NewLine +
                              "Please donate in order to keep us alive! We accept BitCoin, LiteCoin, Ethereum, PitCoin , ShitCoin and any other coin!");

        }
    }
}
