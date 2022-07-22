using System;
using System.Net;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HR.LeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails
            {
                ErrorMessage = ex.Message,
                ErrorType = "Failure"
            });
            switch (ex)
            {
                case BadRequestException badRequest:
                    code = HttpStatusCode.BadRequest;
                    break;
                case ValidationException validation:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validation.Errors);
                    break;
                case NotFoundException notFound:
                    code = HttpStatusCode.NotFound;
                    break;
                default: break;
            }

            httpContext.Response.StatusCode = (int)code;
            return httpContext.Response.WriteAsync(result);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
