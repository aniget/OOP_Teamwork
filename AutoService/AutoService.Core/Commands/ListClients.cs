using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;
using System.Linq;
using System;
using AutoService.Core.Validator;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Enums;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.Common.Models;

namespace AutoService.Core.Commands
{
    public class ListClients : ICommand
    {
        private readonly IDatabase database;
        private readonly IWriter consoleWriter;

        public ListClients(IDatabase database, IWriter writer)
        {
            this.database = database;
            this.consoleWriter = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            var clients = this.database.Clients;
            if (clients.Count == 0)
            {
                throw new ArgumentException($"Oops! Sorry pitch, the are no clients.Go! And find them!");
            }
            foreach (var client in clients)
            {
                consoleWriter.Write(client.ToString());
            }
            
        }
    }
}
