using System;

namespace AutoService.Models.Assets.Events
{
    public class CriticalLimitReachedEventArgs : EventArgs
    {
        public decimal CriticalLimit { get; set; }
    }
}
