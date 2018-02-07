using AutoService.Core.Contracts;
using System;

namespace AutoService.Core.Commands
{
    public class ListClients : ICommand
    {
        private readonly IDatabase database;
        private readonly IWriter writer;

        public ListClients(IDatabase database, IWriter writer)
        {
            this.database = database;
            this.writer = writer;
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            var clients = this.database.Clients;
            if (clients.Count == 0)
            {
                throw new ArgumentException($"Oops! Sorry pitch, the are no clients. Go! And find them!");
            }
            foreach (var client in clients)
            {
                writer.Write(client.ToString());
            }
            
        }
    }
}
