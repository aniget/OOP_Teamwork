using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Common.Models;
using AutoService.Models.Contracts;

namespace AutoService.Models.BusinessProcess.Models
{
    public class OrderStock : Order, IOrderStock
    {
        private IStock stock;

        public OrderStock(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty supplier, IStock stock /*, ICollection<IStock> warehouse*/) : base(responsibleEmployee, price, job, supplier)
        {
            this.stock = stock ?? throw new ArgumentException("Stock cannot be null!");
        }

        public IStock Stock
        {
            get => this.stock;
            protected set => this.stock = value;
        }
        
        public void OrderStockToWarehouse(string employeeFirstName, string supplierName, string stockName, decimal purchasePrice)
        {
            if (string.IsNullOrWhiteSpace(employeeFirstName)) { throw new ArgumentException("Please enter a valid employee name!"); }
            if (string.IsNullOrWhiteSpace(supplierName)) { throw new ArgumentException("Supplier cannot be null!"); }

            Warehouse.AddPartToWarehouse(this.Stock,this.ResponsibleEmployee);
        }

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();
        //    builder.AppendLine($"The following stock {stock} was purchased from supplier {Supplier} and was delivered straight to the warehouse!");
        //    return builder.ToString();
        //}
    }
}
