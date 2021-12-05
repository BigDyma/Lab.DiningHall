using DinningHall.Models;
using DinningHall.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveRequestsController : ControllerBase
    {
        private readonly IDiningHallService _diningHallService;
        public ReceiveRequestsController(IDiningHallService diningHallService)
        {
            this._diningHallService = diningHallService;
        }

        [HttpPost("Order/ready")]
        public async Task<IActionResult >ReceiveOrder(Order order)
        {
           await _diningHallService.ServeOrder(order);
            return Ok();
        }
        [HttpPost("Kitchen/ready")]
        public async Task<IActionResult> Start()
        {
            await _diningHallService.StartWaitersWork();
            return Ok();
        }

        [HttpPost("Kitchen/closed")]
        public  void Stop()
        {
            System.Environment.Exit(1);
        }
    }
}
