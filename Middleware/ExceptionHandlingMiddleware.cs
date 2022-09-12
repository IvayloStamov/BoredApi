using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoredApi.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var status = ex switch
                {
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    DuplicateNameException => HttpStatusCode.Conflict,

                    ApplicationException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError,
                };
                context.Response.StatusCode = (int) status;

                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }
}