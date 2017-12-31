using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;
using AutoService.Models.Vehicles.Models;

namespace AutoService.Models.BusinessProcess.Contracts
{
    public interface ISellStock: ISell/*, IStock*/
    {
        IStock Stock { get; }

        
    }
    
}
