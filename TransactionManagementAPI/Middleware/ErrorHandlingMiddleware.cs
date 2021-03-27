using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TransactionManagementAPI.Middleware
{
    /// <summary>
    /// Middleware for error handling
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// Method for asynchronous context calling
        /// </summary>
        /// <param name="context"> Call context </param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Console.ForegroundColor = ConsoleColor.Red);
                await HandleExceptionAsync(context, ex);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Method for handling exception
        /// </summary>
        /// <param name="context"> Call context </param>
        /// <param name="ex"> Some exception </param>
        /// <returns> Exception.message in JSON format </returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { message = ex.Message });
            return context.Response.WriteAsync(result);
        }
    }
}