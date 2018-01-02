using System;

namespace AutoService.Models.Common
{
    public class CriticalLimitReachedEventArgs : EventArgs
    {
        public decimal CriticalLimit { get; set; }
    }
}
