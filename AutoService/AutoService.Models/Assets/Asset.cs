using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Contracts;

namespace AutoService.Models.Assets
{
    public abstract class Asset : IAsset
    {
        private string name;
        private IEmployee responsibleEmployee;

        public Asset(string name, IEmployee responsibleEmployee)
        {
            this.Name = name;
            this.ResponsibleEmployee = responsibleEmployee;
        }

        public string Name
        {
            get => this.name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Asset name cannot be null!");
                }
                this.name = value;
            }
        }
        public IEmployee ResponsibleEmployee
        {
            get => this.responsibleEmployee;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Null employee provided!");
                }
                this.responsibleEmployee = value;
            }
        }

        protected abstract void ChangeResponsibleEmployee(IEmployee employee);

        public override string ToString()
        {
            return $"Asset type: {this.GetType().Name}" + Environment.NewLine +
                   $"  - Asset name: {this.Name}" + Environment.NewLine +
                $"  - Responsible employee: {this.ResponsibleEmployee.FirstName} {this.ResponsibleEmployee.LastName} - {this.ResponsibleEmployee.Position}";
        }
    }
}
