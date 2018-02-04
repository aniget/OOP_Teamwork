using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService.Models.Common.Models
{
    public class Warehouse : IWarehouse
    {
        private List<IStock> availableStocks;

        public Warehouse()
        {
            this.availableStocks = new List<IStock>();
        }

        public List<IStock> AvailableStocks => this.availableStocks;


        public override string ToString()
        {
            var sb = new StringBuilder();

            var counter = 1;

            foreach (var stock in this.AvailableStocks.OrderBy(ob => ob.Supplier.Name))
            {
                sb.AppendLine(counter + ". " + stock + Environment.NewLine);
                counter++;
            }
            return sb.ToString();
        }
    }
}
