using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class OrderStock : Order, IOrderStock
    {
        private readonly IStock stock;
        private readonly ICollection<IStock> warehouse;

        public OrderStock(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty supplier, ICollection<IStock> warehouse) : base(responsibleEmployee, price, job, supplier)
        {

            this.warehouse = warehouse;
        }

        public IStock Stock => this.stock;

        public void OrderStockToWarehouse(IEmployee responsibleEmployee, ICounterparty supplier, IStock stock,
            ICollection<IStock> warehouse)
        {
            if (responsibleEmployee == null) { throw new ArgumentException("Please enter employee!"); }
            if (responsibleEmployee.IsHired == false) { throw new ArgumentException($"Employee {responsibleEmployee} is no longer working for the AutoService!"); }
            if (responsibleEmployee.Responsibiities.Contains(ResponsibilityType.SellService) == false) { throw new ArgumentException($"Employee {responsibleEmployee} not authorized to make orders of stock"); }
            if (supplier == null) { throw new ArgumentException("Supplier cannot be null!"); }

            warehouse.Add(stock);

        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"The following stock {stock} was purchased from supplier {Supplier} and was delivered straight to the warehouse!");
            return builder.ToString();
        }
    }
}
