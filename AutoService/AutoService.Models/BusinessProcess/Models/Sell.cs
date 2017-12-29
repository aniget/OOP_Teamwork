using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using AutoService.Models.Assets;
using AutoService.Models.Vehicles.Models;
using IEmployee = AutoService.Models.Contracts.IEmployee;

namespace AutoService.Models.BusinessProcess.Models
{
    public abstract class Sell : Work, ISell
    {
        private ICollection<IInvoice> invoices;



        protected Sell(IEmployee responsibleEmployee, decimal price, TypeOfWork job, ICounterparty client, Vehicle vehicle, ICollection<IInvoice> invoices)
            : base(responsibleEmployee, price, job)
        {
            Client = client ?? throw new ArgumentException("Client cannot be null");
            Vehicle = vehicle ?? throw new ArgumentException("Vehicle cannot be null");
            this.invoices = invoices;
        }

        public ICounterparty Client { get; }
        public Vehicle Vehicle { get; }

        public virtual void SellToClientVehicle(IEmployee responsibleEmployee, IClient client, Vehicle vehicle, string invoiceNumber, ISell sell)
        {
            //employee is hired and has the responsibility to sell service
            if (responsibleEmployee == null) { throw new ArgumentException("Please enter employee!"); }
            if (responsibleEmployee.IsHired == false) { throw new ArgumentException($"Employee {responsibleEmployee} is no longer working for the AutoService!"); }
            if (responsibleEmployee.Responsibiities.Contains(ResponsibilityType.SellService) == false) { throw new ArgumentException($"Employee {responsibleEmployee} not authorized to repair vehicles"); }
            if (client == null) { throw new ArgumentException("Client cannot be null!"); }
            if (vehicle == null) { throw new ArgumentException("Vehicle cannot be null!"); }
            if (string.IsNullOrWhiteSpace(invoiceNumber) || string.Empty == invoiceNumber) { throw new ArgumentException("InvoiceNumber cannot be null or empty. Please first create invoice!"); }

            if (string.IsNullOrWhiteSpace(invoiceNumber) || string.Empty == invoiceNumber) //is the invoice number null or empty
            {
                //create the invoice and add the sold service item to it
                //IInvoice invoice = new Invoice(invoiceNumber, client);
                //invoices.Add(invoice);
                //invoice.InvoiceItems.Add(this);
            }
            else //does the invoiceNumber not exist inside invoice collection
            {
                if (!invoices.Any(x => x.Number == invoiceNumber))
                {
                    throw new ArgumentException("No such invoice");
                }
                else //such invoice exists
                {
                    IInvoice existingInvoice = invoices.FirstOrDefault(x => x.Number == invoiceNumber);
                    //is the invoice not issued on our client
                    if (existingInvoice.Client != client)
                    {
                        throw new ArgumentException("No such invoice attached to the current client!");
                    }
                    else //invoice existin and is assigned to our client
                    {
                        //add the Service sold as a new item in the invoice
                        existingInvoice.InvoiceItems.Add(sell);
                    }
                }
            }
        }


        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Information about sale of {0} to client {1} for vehicle {2}" + Environment.NewLine + "Performed by: {3}",
                AdditionalInfo_ServiceOrPart(), Client.Name, Vehicle.Make + " " + Vehicle.Model, base.ResponsibleEmployee));
            return builder.ToString();
        }

        public abstract string AdditionalInfo_ServiceOrPart();

        public abstract decimal CalculateRevenue();

    }
}
