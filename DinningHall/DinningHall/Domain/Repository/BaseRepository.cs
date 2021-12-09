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
        private DinningContext _dinningContext { get; }

        public BaseRepository(DinningContext dinningContext)
        {
            _dinningContext = dinningContext;
        }

        public async Task InitContext()
        {
            _dinningContext.InitContext();
            await SetClientsForAllTables();
        }

        protected async Task SetClientsForAllTables()
        {
            foreach (var table in _dinningContext.Tables)
            {
                await GetClients(table);
            }
        }

        protected async Task GetClients( Table table)
        {
           await Task.Run(async () =>
            {
                var time = new Random().Next(2, 4);
                Thread.Sleep(time);
                table.State = TableState.Available;
                await  UpdateTable(table);
            });
        }
        public  async Task ServeOrder(Order order)
        {
            var table = _dinningContext.Tables.First(t => t.Id == order.TableId);
            float waitTime = (DateTime.Now.Ticks - table.orderedAt.Ticks) / (10000 * 1000);

            Console.WriteLine($"Table {table.Id} received order {order.Id}:");

            Assessor.Assess(waitTime, order);
            _dinningContext.Orders = _dinningContext.Orders.Where(o => o.Id != order.Id).ToList();

            await GetClients(table);
        }
        public async Task<List<Waiter>> GetAvailableWaiters()
        {
            return await Task.FromResult(_dinningContext.Waiters.Where(x => x.State == Models.WaiterState.Available).ToList());
        }
        public async Task<List<Table>> GetTables()
        {
            return await Task.FromResult(_dinningContext.Tables);
        }

        public async Task<Table> UpdateTable(Table table)
        {
            _dinningContext.Tables.Where(x => x.Id == table.Id).ToList().ForEach(x => x = table);

            return await Task.FromResult(table);
        }

        public async Task<Order> SetOrder(Waiter waiter, Table table)
        {
            waiter.State = WaiterState.TakingOrder;
            var time = new Random().Next(2, 4);
            Thread.Sleep(time);
            Console.WriteLine($"Table {table.Id} is making order for {time} ms");

            var order = new Order();

            int amount = new Random().Next(1, 4);

            for (int i = 0; i < amount; i++)
            {
                order.Items.Add(_dinningContext.Menu[new Random().Next(1, _dinningContext.Menu.Count - 1)]);
            }
            order.MaxWaitTime = order.Items.Select(o => _dinningContext.Menu.Find(i => i.Id == o.Id).PreparationTime).OrderByDescending(t => t).First() * 1.3f;
            order.TableId = table.Id;
            order.Priority = DinningHallUtils.GeneratePriority(order);

            table.State = TableState.WaitingForOrder;
            await UpdateTable(table);
            await AddOrder(order);
            return order;
        }

        public  Task<List<Order>> AddOrder(Order order)
        {
            _dinningContext.Orders.Add(order);

            return Task.FromResult(_dinningContext.Orders);
        }
      }
 }

