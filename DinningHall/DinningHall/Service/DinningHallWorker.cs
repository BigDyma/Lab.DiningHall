using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DinningHall.Domain.Repository;

namespace DinningHall.Service
{
    public class DinningHallWorker : BackgroundService
    {
        private readonly IWaiterService _waiterService;
        private readonly ILogger<DinningHallWorker> _logger;
        public DinningHallWorker(IWaiterService waiterService, ILogger<DinningHallWorker> logger)
        {
            _waiterService = waiterService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _waiterService.StartWaitersWork(stoppingToken);
        }
    }
}
