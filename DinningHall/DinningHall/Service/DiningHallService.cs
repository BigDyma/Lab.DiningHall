using DinningHall.Domain.Repository;
using DinningHall.Models;
using DinningHall.Models.Domain;
using DinningHall.Utils;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Service
{

    public class DiningHallService : IDiningHallService
    {
        public readonly IBaseRepository baseRepository;

        private readonly IRequestService Server;

        private static Mutex mutex = new Mutex();

        public DiningHallService(IRequestService server, IBaseRepository baseRepository)
        {
            Server = server;
            this.baseRepository = baseRepository;
            StartWaitersWork().GetAwaiter().GetResult();
        }
        public async Task ServeOrder(Order order)
        {
            if (order is object)
               await baseRepository.ServeOrder(order);
        }
        public async Task StartWaitersWork()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(100);
                new Thread(delegate()
                {
                    while (true)
                    {
                        var availableWaiters = baseRepository.GetAvailableWaiters();

                        foreach (var waiter in availableWaiters)
                        {
                            foreach (var table in baseRepository.GetTables())
                            {
                                mutex.WaitOne();
                                if (table.State == TableState.Available)
                                {
                                    Console.WriteLine($"Waiter {waiter.Id} approched the Table {table.Id}");
                                    table.State = TableState.Ordering;
                                    baseRepository.UpdateTable(table);
                                    mutex.ReleaseMutex();
                                    Order order = baseRepository.SetOrder(waiter, table);
                                    table.orderedAt = DateTime.Now;
                                    baseRepository.UpdateTable(table);
                                    baseRepository.AddOrder(order);

                                    Server.SendOrder(waiter, order, table);
                                }
                                else
                                    mutex.ReleaseMutex();
                            }
                        }
                    }
                }).Start();
            });

        }



    }
}
