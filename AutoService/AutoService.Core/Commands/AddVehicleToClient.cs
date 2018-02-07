using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Core.Contracts;

namespace AutoService.Core.Commands
{
    public class AddVehicleToClient : ICommand
    {
        public AddVehicleToClient()
        {
            
        }
        public void ExecuteThisCommand(string[] commandParameters)
        {
            throw new NotImplementedException();
        }
    }
}
