using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreRateLimit;
using HR.LeaveManagement.Persistence;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HR.LeaveManagement.API
{
    public static class ServiceExtensions
    {
        //public static void ConfigureIdentity(this IServiceCollection services)
        //{
        //    var builder = services.AddIdentityCore<IdentityUser>(q => q.User.RequireUniqueEmail = true);
        //    builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
        //    builder.AddTokenProvider("HotelListingApi", typeof(DataProtectorTokenProvider<IdentityUser>));
        //    builder.AddEntityFrameworkStores<LeaveManagementDbContext>().AddDefaultTokenProviders();
        //}

     
        public static void ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "0auth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "HR Leave Management API"
                });
            });

        }
        //public static void ConfigureException(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(x =>
        //    {
        //        x.Run(async context =>
        //        {
        //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //            context.Response.ContentType = "application/json";
        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                //Log.Error($"Something went wrong in the {nameof(contextFeature.Error)}");
        //                await context.Response.WriteAsync(new Error
        //                {
        //                    StatusCode = context.Response.StatusCode,
        //                    Message = "Internal Server Error. Please try again later.",
        //                    Success = false,
        //                    Data = null,
        //                }.ToString());
        //            }

        //        });
        //    });
        //}

        public static void ConfigureVersion(this IServiceCollection service)
        {

            service.AddApiVersioning(x =>
            {
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.DefaultApiVersion = new ApiVersion(1, 0);
                //x.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }
        public static void ConfigureHttpCacheHeaders(this IServiceCollection service)
        {

            service.AddResponseCaching();
            service.AddHttpCacheHeaders(x =>
            {
                x.MaxAge = 5; //seconds
                x.CacheLocation = CacheLocation.Public;
            },
                y =>
                {
                    y.MustRevalidate = true;
                });
        }

        public static void ConfigureRateLimiting(this IServiceCollection service)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 10,
                    Period = "10s",
                },
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "1h",
                    Limit = 3600,
                }
            };

            service.Configure<IpRateLimitOptions>(x =>
            {
                x.GeneralRules = rateLimitRules;
            });
            service.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            service.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        }
    }
}
