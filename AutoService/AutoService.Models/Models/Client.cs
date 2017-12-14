using System;
using AutoService.Models.Contracts;

namespace AutoService.Models.Models
{
    public class Client : Company, IClient
    {
        private int dueDaysAllowed;
        public Client(string name, string address, string uniqueNumber, decimal creditLimit, int dueDaysAllowed) : base(name, address, uniqueNumber, creditLimit)
        {
            if (dueDaysAllowed < 0)
            {
                throw new ArgumentException("Due days cannot be negative!");
            }
            this.dueDaysAllowed = dueDaysAllowed;
        }

        public Client(string name, string address, string uniqueNumber, decimal creditLimit, int dueDaysAllowed, string taxNumber) : base(name, address, uniqueNumber, creditLimit, taxNumber)
        {
            if (dueDaysAllowed < 0)
            {
                throw new ArgumentException("Due days cannot be negative!");
            }
            this.dueDaysAllowed = dueDaysAllowed;
        }

        public int DueDaysAllowed { get { return this.dueDaysAllowed; } }

        public void UpdateDueDays(int dueDays)
        {
            if (dueDays < 0)
            {
                throw new ArgumentException("Due days cannot be negative!");
            }
            this.dueDaysAllowed = dueDays;
        }

        public void SendReminderForOutstandingInvoice()
        {
            throw new NotImplementedException();
        }
    }
}
