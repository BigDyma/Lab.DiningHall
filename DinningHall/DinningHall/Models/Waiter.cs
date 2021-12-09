using DinningHall.Models.Domain;
using DinningHall.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Models
{
    public class Waiter : BaseEntity
    {
        public WaiterState State { get; set; }

        public Waiter(): base()
        {

        }

    }
}
