using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models.Domain
{
    public enum TableState
    {
        Available,
        WaitingForWaiter,
        Ordering,
        WaitingForOrder,
    }
}
