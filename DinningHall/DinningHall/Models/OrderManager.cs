using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class OrderManager : Order
    {
        public Guid TableId { get; set; }
    }
}
