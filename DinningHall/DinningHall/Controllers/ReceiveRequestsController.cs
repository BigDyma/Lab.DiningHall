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
        private readonly IDiningHallService diningHallService;
        public ReceiveRequestsController(IDiningHallService diningHallService)
        {
            this.diningHallService = diningHallService;
        }

        [HttpPost("Order/ready")]
        public IActionResult ReceiveOrder(Order order)
        {
            diningHallService.ServeOrder(order);
            return Ok();
        }
        [HttpPost("Kitchen/ready")]
        public IActionResult Start()
        {
            diningHallService.StartWaitersWork();
            return Ok();
        }
    }
}
