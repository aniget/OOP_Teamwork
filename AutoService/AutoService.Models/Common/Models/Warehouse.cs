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
        
        //public Warehouse()
        //{
        //}

        //private IList<IStock> availableStocks;

        public Warehouse()
        {
            this.AvailableStocks = new List<IStock>();
        }

        public IList<IStock> AvailableStocks { get; set; }


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
