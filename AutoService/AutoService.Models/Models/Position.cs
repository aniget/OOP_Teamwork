using System;

namespace AutoService.Models.Models
{
    public class Position
    {
        private string name;

        public Position(string name)
        {
            this.Name = name;
        }

        public string Name {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid position!");
                }
                this.name = value;
            }
        }
    }
}
