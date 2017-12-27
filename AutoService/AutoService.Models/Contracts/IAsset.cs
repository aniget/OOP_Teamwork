using System;

namespace AutoService.Models.Contracts
{
    public interface IAsset
    {
        string Name { get; }

        IEmployee ResponsibleEmployee { get; }

        DateTime RegistrationDate { get; }
    }
}
