using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall
{
    public class HandleExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleExceptionsMiddleware> _logger;
        public HandleExceptionsMiddleware(RequestDelegate next, ILogger<HandleExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await context.Response.WriteAsync(ex.Message);

            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
