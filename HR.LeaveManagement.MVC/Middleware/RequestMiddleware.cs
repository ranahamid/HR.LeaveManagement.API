using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace HR.LeaveManagement.MVC.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILocalStorageService _localStorageService;
        public RequestMiddleware(RequestDelegate next, ILocalStorageService localStorageService)
        {
            _next = next;
            _localStorageService = localStorageService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var endPoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
                var authAttr = endPoint?.Metadata?.GetMetadata<AuthorizeAttribute>();
                if (authAttr != null)
                {
                    var tokenExists = _localStorageService.Exist("token");
                    var tokenIsValid = true;
                    if (tokenExists)
                    {
                        var token = _localStorageService.GetStorageValue<string>("token");
                        JwtSecurityTokenHandler tokenHandler = new();
                        var tokenContent = tokenHandler.ReadJwtToken(token);
                        var expire = tokenContent.ValidTo;
                        if (expire < DateTime.Now)
                        {
                            tokenIsValid = false;
                        }
                    }

                    if (!tokenIsValid || !tokenExists)
                    {
                        await SignOutAndRedirect(httpContext);
                        return;
                    }

                    if (authAttr.Roles != null)
                    {
                        var userRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                        if (!authAttr.Roles.Contains(userRole))
                        {
                            var path = "/Home/NotAuthorized";
                            httpContext.Response.Redirect(path);
                            return;
                        }
                    }
                }

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
           
            switch (ex)
            {
                case ApiException apiException:
                    await SignOutAndRedirect(httpContext);
                    break;
                default:
                    var path = "/Home/Error";
                    httpContext.Response.Redirect(path);
                    break;
            } 
        }

        private static async Task SignOutAndRedirect(HttpContext httpContext)
        {

        }
    }
}
