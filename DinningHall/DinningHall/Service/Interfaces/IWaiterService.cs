using DinningHall.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public interface IWaiterService
    {
        Task StartWaitersWork(CancellationToken stoppingToken);
    }
}