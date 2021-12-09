using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class Order : BaseEntity
    {
        public List<Food> Items = new List<Food>();
        public int Priority { get; set; }
        public double MaxWait { get; set; }
        public float MaxWaitTime { get; set; }
        public Guid TableId { get; set; }

        public Order() : base()
        {

        }

    }
}
