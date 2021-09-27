using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DinningHallController : ControllerBase
    {

        private readonly ILogger<DinningHallController> _logger;

        public DinningHallController(ILogger<DinningHallController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Post()
        {
        }
    }
}
