using DinningHall.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class Table : BaseEntity
    {
        public TableState State { get; set; }
        public DateTime orderedAt { get; set; }

        public Table(): base()
        {

        }
    }
}
