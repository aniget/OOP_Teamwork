using System;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Assets
{
    public abstract class Asset : IAsset
    {
        private string name;
        private IEmployee responsibleEmployee;
        private string uniqueNumber;

        public Asset(string name, IEmployee responsibleEmployee, string uniqueNumber)
        {
            this.Name = name;
            this.ResponsibleEmployee = responsibleEmployee;
            this.UniqueNumber = uniqueNumber;
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

                if (value.Length > 200)
                {
                    throw new ArgumentException("Asset name cannot be more than 200 characters long!");
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

        public string UniqueNumber
        {
            get => this.uniqueNumber;

            protected set
            {
                if (value == null)
                {
                    throw new ArgumentException("Unique number cannot be null!");
                }

                this.uniqueNumber = value;
            }
        }

        protected abstract void ChangeResponsibleEmployee(IEmployee employee);

        public override string ToString()
        {
            return $"Asset type: {this.GetType().Name}" + Environment.NewLine +
                   $"  - Asset name: {this.Name}" + Environment.NewLine +
                   $"  - Asset unique number: {this.UniqueNumber}" + Environment.NewLine +
                $"  - Responsible employee: {this.ResponsibleEmployee.FirstName} {this.ResponsibleEmployee.LastName} - {this.ResponsibleEmployee.Position}";
        }
    }
}
