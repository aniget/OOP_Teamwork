using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Models.Contracts
{
    public interface IAsset
    {
        
        string Name { get; }
        DateTime RegistrationDate { get; }
        
     }
}
