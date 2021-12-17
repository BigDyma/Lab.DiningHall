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
    public class BaseRepository : IBaseRepository, IContextInitializator
    {
        private DinningContext _dinningContext { get; }

        private static  SemaphoreLocker _locker = new SemaphoreLocker();

        public BaseRepository(DinningContext dinningContext)
        {
            this._dinningContext = dinningContext;
        }

        public async Task SetClientsForAllTables()
        {

            foreach (var table in _dinningContext.Tables)
            {
                Console.WriteLine($"{table.Id} is leaving... Grade 0");
                await GetClients(table);
            }
        }

        protected async Task GetClients(Table table)
        {

                        var time = new Random().Next(2, 4);
                        Thread.Sleep(time);
                        table.State = TableState.Available;
                        var t = await UpdateTable(table);

                        Console.WriteLine($"table {t.Id} is now available");
        }
    

        public async Task ServeOrder(Order order)
        {

            var table = _dinningContext.Tables.First(t => t.Id == order.TableId);
            float waitTime = (DateTime.Now.Ticks - table.orderedAt.Ticks) / (10000 * 1000);

            Console.WriteLine($"Table {table.Id} received order {order.Id}:");

            Assessor.Assess(waitTime, order);

           await  RemoveOrder(order);

            await GetClients(table); 
        }

    private async Task RemoveOrder(Order order)
    {
    await _locker.LockAsync( () =>
    {
         _dinningContext.Orders = _dinningContext.Orders.Where(o => o.Id != order.Id).ToList();
         return Task.CompletedTask;
    });     
    }

    public async Task<List<Waiter>> GetAllWaiters()
    {
       return await _locker.LockAsync(() =>
        {
            return Task.FromResult(_dinningContext.Waiters);
        });
    }

        public async Task<List<Waiter>> GetAvailableWaiters()
        {
            var allTables = await GetAllWaiters();

            return allTables.Where(x => x.State == Models.WaiterState.Available)
                .ToList();

        }
        public async Task<List<Table>> GetTables()
        {
           // return await _locker.LockAsync(async () =>
             return   await Task.FromResult(_dinningContext.Tables);
                    //);
        }

        public async Task<Table> UpdateTable(Table table)
        { 

                await Task.Run(() =>
                {
                    for (var i = 0; i < _dinningContext.Tables.Count; i++)
                    {
                        if (_dinningContext.Tables[i].Id == table.Id)
                        {
                            _dinningContext.Tables[i] = table;
                            Console.WriteLine($"{table.Id} was update to state {table.State}");
                        }
                    }
                });
                return table;
            

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

        public async Task<List<Order>> AddOrder(Order order)
        {

                await Task.Run(() =>
                    _dinningContext.Orders.Add(order));

                return _dinningContext.Orders;
        }
    }
}

