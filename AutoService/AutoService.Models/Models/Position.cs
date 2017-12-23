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
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                {
                    throw new ArgumentException("Invalid position, must be at least 2 characters!");
                }
                this.name = value;
            }
        }
    }
}
