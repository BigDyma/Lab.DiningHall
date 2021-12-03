using DinningHall.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public interface IDiningHallService
    {
        Task StartWaitersWork();
        Task ServeOrder(Order order);
    }
}