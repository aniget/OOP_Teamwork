using AutoService.Models.Contracts;

namespace AutoService.Models.Assets.Contracts
{
    public interface IAsset
    {
        string Name { get; }

        IEmployee ResponsibleEmployee { get; }

    }
}
