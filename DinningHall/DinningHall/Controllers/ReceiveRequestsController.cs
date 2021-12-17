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
    [Route("api")]
    [ApiController]
    public class ReceiveRequestsController : ControllerBase
    {
        private readonly IDinningHallService _dinningHall;
        public ReceiveRequestsController(IDinningHallService dinningHall)
        {
            this._dinningHall = dinningHall;
        }

        [HttpPost("Order/ready")]
        public async Task<IActionResult >ReceiveOrder(Order order)
        {
           await _dinningHall.ServeOrder(order);
            return Ok();
        }
    }
}
