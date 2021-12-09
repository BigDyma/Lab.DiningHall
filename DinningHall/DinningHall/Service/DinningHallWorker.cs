using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public class DinningHallWorker : BackgroundService
    {
        private readonly IDiningHallService _diningHallService;
        private readonly ILogger<DinningHallWorker> _logger;

        public DinningHallWorker(IDiningHallService diningHallService, ILogger<DinningHallWorker> logger)
        {
            _diningHallService = diningHallService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("$Worker running ");
            await _diningHallService.StartWaitersWork(stoppingToken);

        }
    }
}
