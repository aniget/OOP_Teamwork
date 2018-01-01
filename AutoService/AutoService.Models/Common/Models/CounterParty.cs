using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Common.Models
{
    public abstract class CounterParty : ICounterparty
    {
        protected string name;
        private string address;
        private string uniqueNumber;
        private ICollection<IInvoice> invoices;
        
        public CounterParty(string name, string address, string uniqueNumber)
        {
            this.Name = name;
            this.Address = address;
            this.UniqueNumber = uniqueNumber;
            this.invoices = new List<IInvoice>();
        }

        public string Name
        {
            get => this.name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException("Company name must be more than 5 symbols!");
                }
                this.name = value;
            }
        }

        public string Address
        {
            get => this.address;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException("Address must be more than 5 symbols!");
                }
                this.address = value;
            }
        }

        public string UniqueNumber
        {
            get => this.uniqueNumber;
            protected set
            {
                if (value.Any(a => !char.IsDigit(a)) || string.IsNullOrWhiteSpace(value) || value.Length != 9)
                {
                    throw new ArgumentException("Invalid unique number, must be exactly nine numbers!");
                }
                this.uniqueNumber = value;
            }
        }

        public ICollection<IInvoice> Invoices
        {
            get => this.invoices;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name} name: {this.Name}");
            sb.AppendLine($"### Address: {this.Address}");
            sb.AppendLine($"### UIN: {this.UniqueNumber}");
            sb.AppendLine("### Invoices:");
            if (invoices.Count == 0)
            {
                sb.Append("No invoices!");
            }
            else
            {
                foreach (var invoice in invoices)
                {
                    sb.AppendLine("#" + Environment.NewLine + invoice + Environment.NewLine + "#");
                }
            }
            return sb.ToString();
        }
    }
}
