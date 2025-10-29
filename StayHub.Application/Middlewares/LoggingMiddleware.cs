using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _logger = logger; 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IActionRepository actionRepository)
        {
            _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);
            await _next(context); // Call the next middleware in the pipeline
            _logger.LogInformation("Outgoing response status: {StatusCode}", context.Response.StatusCode);

        }
    }
}
