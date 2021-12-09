using DinningHall.Models;
using DinningHall.Utils;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall
{
    public class DinningContext : IDinningContext
    {
        public List<Food> Menu { get; set; }
        public List<Table> Tables { get; set; }
        public List<Order> Orders { get; set; }
        public List<Waiter> Waiters { get; set; }

        public DinningContext()
        {
            InitContext();
            
        }

        public void InitContext()
        {
            InitMenu();
            InitTables();
            InitWaiters();
            Orders = new List<Order>();
        }
        protected void InitMenu()
        {
            Menu = DinningHallUtils.getMenu();
        }
        protected void InitWaiters()
        {
            Waiters = DinningHallUtils.GetWaiters(5);

        }
        protected void InitTables()
        {
            Tables = DinningHallUtils.GetTables(5);
        }

    }
}
