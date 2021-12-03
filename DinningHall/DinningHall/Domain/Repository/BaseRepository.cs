using DinningHall.Models;
using DinningHall.Models.Domain;
using DinningHall.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Domain.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private DinningContext dinningContext { get; }

        public BaseRepository(DinningContext dinningContext)
        {
            this.dinningContext = dinningContext;
            SetClientsForAllTables();
        }

        protected void SetClientsForAllTables()
        {
            foreach (var table in dinningContext.Tables)
            {
                GetClients(table);
            }
        }

        public async Task GetClients( Table table)
        {
           await Task.Run(() =>
            {
                var time = new Random().Next(2, 4);
                Thread.Sleep(time);
                table.State = TableState.Available;
                UpdateTable(table);
            });
        }
        public  async Task ServeOrder(Order order)
        {
            var table = dinningContext.Tables.First(t => t.Id == order.TableId);
            float waitTime = (DateTime.Now.Ticks - table.orderedAt.Ticks) / (10000 * 1000);

            Console.WriteLine($"Table {table.Id} received order {order.Id}:");

            Assessor.Assess(waitTime, order);
            dinningContext.Orders = dinningContext.Orders.Where(o => o.Id != order.Id).ToList();

            await GetClients(table);
        }
        public List<Waiter> GetAvailableWaiters()
        {
            return dinningContext.Waiters.Where(x => x.State == Models.WaiterState.Available).ToList();
        }
        public List<Table> GetTables()
        {
            return dinningContext.Tables;
        }

        public Table UpdateTable(Table table)
        {
            dinningContext.Tables.Where(x => x.Id == table.Id).ToList().ForEach(x => x = table);

            return table;
        }

        public Order SetOrder(Waiter waiter, Table table)
        {
            waiter.State = WaiterState.TakingOrder;
            var time = new Random().Next(2, 4);
            Thread.Sleep(time);
            Console.WriteLine($"Table {table.Id} is making order for {time} ms");

            var order = new Order();

            int amount = new Random().Next(1, 4);

            for (int i = 0; i < amount; i++)
            {
                order.Items.Add(dinningContext.Menu[new Random().Next(1, dinningContext.Menu.Count - 1)]);
            }
            order.MaxWaitTime = order.Items.Select(o => dinningContext.Menu.Find(i => i.Id == o.Id).PreparationTime).OrderByDescending(t => t).First() * 1.3f;
            order.TableId = table.Id;
            order.Priority = DinningHallUtils.GeneratePriority(order);

            table.State = TableState.WaitingForOrder;
            UpdateTable(table);
            dinningContext.Orders.Add(order);
            return order;
        }

        public List<Order> AddOrder(Order order)
        {
            dinningContext.Orders.Add(order);

            return dinningContext.Orders;
        }
      }
 }

