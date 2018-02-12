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
            this.database = database ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
        }

        public void ExecuteThisCommand(string[] commandParameters)
        {
            var clients = this.database.Clients;
            if (clients.Count == 0)
            {
                throw new ArgumentException($"Oops! Sorry pitch, there are no clients. Go and find them!");
            }
            foreach (var client in clients)
            {
                writer.Write(client.ToString());
            }
            
        }
    }
}
