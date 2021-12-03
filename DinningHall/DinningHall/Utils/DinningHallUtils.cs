using DinningHall.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Service;

namespace DinningHall.Utils
{
    public abstract class DinningHallUtils
    {
        public static List<Waiter> GetWaiters(int waiterCount)
        {
            List<Waiter> waiters = new List<Waiter>();
            waiters.AddRange((new Waiter[waiterCount]).Select(o => new Waiter()));
            return waiters.ToList(); 
        }
        public static List<Food> getMenu()
        {
            return LoadJson();
        }
        public static List<Food> LoadJson()
        {
            using (StreamReader r = new StreamReader("Menu.json"))
            {
                string json = r.ReadToEnd();
                List<Food> items = JsonSerializer.Deserialize<List<Food>>(json);
                return items;
            }
        }
        public static List<Table> GetTables( int amount)
        {
            List<Table> t = new List<Table>();
             t.AddRange((new Table[amount]).Select(o => new Table()));

            return t;
        }

        public static int GeneratePriority(Order order)
        {
            var priority = 1;
            if ((new int[] { 1, 4 }).ToList().Contains(order.Items.Count))
                priority += 2;

            if (order.MaxWaitTime <= 30)
                priority += 1;

            return priority;
        }

    }
}
