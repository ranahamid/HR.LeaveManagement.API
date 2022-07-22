using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
         
            services.AddMediatR(Assembly.GetExecutingAssembly());
           // services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}