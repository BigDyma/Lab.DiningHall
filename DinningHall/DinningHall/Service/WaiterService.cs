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
using Microsoft.Extensions.Logging;

namespace DinningHall.Service
{
    public class WaiterService : IWaiterService
    {
        private readonly IBaseRepository _baseRepository;

        private readonly IRequestService _server;

        private readonly ILogger<WaiterService> _logger;

        private static SemaphoreLocker _locker = new SemaphoreLocker();
        
        public WaiterService(IRequestService server, IBaseRepository baseRepository,
            ILogger<WaiterService> logger)
        {
            _server = server;
            _baseRepository = baseRepository;
            _logger = logger;
        }
        
        private async Task StartWaitersWork()
        {
            var availableWaiters = await _baseRepository.GetAllWaiters();

            var availableTables = await _baseRepository.GetTables().ConfigureAwait(false);
            
                await Task.Run(async () =>
                {
                    Parallel.For(0, availableWaiters.Count, async (i) =>
                    {
                    //foreach (var waiter in availableWaiters)
                    await FindAvailableTables(availableWaiters[i]).ConfigureAwait(false);
                    });
                });

        }

        private async Task FindAvailableTables( Waiter waiter)
        {
            var tables = await _baseRepository.GetTables().ConfigureAwait(false);
            
            for (var i = 0 ; i < tables.Count; i++)
            {
                var ctables = await _baseRepository.GetTables().ConfigureAwait(false);

                var table = ctables[i];
                var wasAvailable = await ApproachToAvailableTable(waiter, table).ConfigureAwait(false);
                
                if (wasAvailable)
                    await CreateOrder(waiter, table).ConfigureAwait(false);
            }
        }

        private async Task<bool> ApproachToAvailableTable(Waiter waiter, Table table)
        {
           return await _locker.LockAsync(async () =>
           {
               var wasAvailable = table.State == TableState.Available;
                if (wasAvailable)
                {
                    Console.WriteLine($"Waiter {waiter.Id} approched the Table {table.Id}");
                    table.State = TableState.Ordering;
                    await _baseRepository.UpdateTable(table);
                }

                return wasAvailable;
            });
        }
        private async Task CreateOrder(Waiter waiter, Table table)
        {
            Console.WriteLine($"Waiter {waiter.Id} approched the Table {table.Id} - was available");

            Order order = await _baseRepository.SetOrder(waiter, table);
            table.orderedAt = DateTime.Now;
            await _baseRepository.UpdateTable(table);
            await _baseRepository.AddOrder(order);
            await _server.SendOrder(waiter, order, table).ConfigureAwait(false);
        }

        public async Task StartWaitersWork(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Brigadir is sending waiters to work");
                await StartWaitersWork();
                await Task.Delay(10000);
            }
        }
    }
}