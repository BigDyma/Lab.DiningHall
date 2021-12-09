using DinningHall.Models;
using System.Collections.Generic;

namespace DinningHall
{
    public interface IDinningContext
    {
        List<Food> Menu { get; set; }
        List<Order> Orders { get; set; }
        List<Table> Tables { get; set; }
        List<Waiter> Waiters { get; set; }
    }
}