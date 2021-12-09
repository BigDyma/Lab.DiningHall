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

    public class DiningHallService :  IDiningHallService
    {
        public readonly IBaseRepository _baseRepository;

        private readonly IRequestService _server;

        public DiningHallService(IRequestService server, IBaseRepository baseRepository)
        {
            _server = server;
            _baseRepository = baseRepository;
        }
        public async Task ServeOrder(Order order)
        {
            if (order is object)
               await _baseRepository.ServeOrder(order);
        }
        public async Task StartWaitersWork()
        {
            var availableWaiters = await _baseRepository.GetAvailableWaiters();
            var tables = await  _baseRepository.GetTables();

            await Task.Run(async () =>
            {
            //Parallel.ForEach(availableWaiters, (waiter) =>
            //{
            foreach (var waiter in availableWaiters)
            {
                foreach (var table in tables)
                    {
                        if (table.State == TableState.Available)
                        {
                            Console.WriteLine($"Waiter {waiter.Id} approched the Table {table.Id}");
                            table.State = TableState.Ordering;
                            await _baseRepository.UpdateTable(table);
                            Order order = await _baseRepository.SetOrder(waiter, table);
                            table.orderedAt = DateTime.Now;
                            await _baseRepository.UpdateTable(table);
                            await _baseRepository.AddOrder(order);
                            _server.SendOrder(waiter, order, table).ConfigureAwait(false).GetAwaiter().GetResult();
                        }
                    }
               // });
            }
            }
            );


        }

        public async Task StartWaitersWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await StartWaitersWork();
                await Task.Delay(1000);
            }
        }
    }
}
