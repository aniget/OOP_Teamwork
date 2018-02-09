using System;
using System.Linq;
using System.Text;
using AutoService.Core.Contracts;
using AutoService.Models.Common.Contracts;

namespace AutoService.Core.Manager
{
     public class InvoiceManager : IInvoiceManager
     {
         private IInvoice invoice;

         public void SetInvoice(IInvoice invoice)
         {
             this.invoice = invoice;
         }

        public void IncreasePaidAmount(decimal amount)
        {
            this.invoice.PaidAmount += amount;
        }
        public void CalculateInvoiceAmount()
        {
            this.invoice.Amount = this.invoice.InvoiceItems.Select(i => i.SellPrice).Sum();
        }
    }
}
