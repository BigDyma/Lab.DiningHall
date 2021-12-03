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
    public class DinningContext : BackgroundService
    {
        public List<Food> Menu { get; set; }
        public List<Table> Tables { get; set; }
        public List<Order> Orders { get; set; }
        public List<Waiter> Waiters { get; set; }

        public DinningContext()
        {
            Menu = DinningHallUtils.getMenu();
            Waiters = DinningHallUtils.GetWaiters(5);
            Tables = DinningHallUtils.GetTables(5);
        }
        protected void InitTables()
        {
            Menu = DinningHallUtils.getMenu();
        }
        protected void InitWaiters()
        {
            Tables = DinningHallUtils.GetTables(5);
        }
    
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitTables();
            InitWaiters();
            return Task.CompletedTask;
        }
    }
}
