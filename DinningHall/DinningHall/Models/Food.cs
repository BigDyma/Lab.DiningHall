using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public int Complexity { get; set; }
    }
}
