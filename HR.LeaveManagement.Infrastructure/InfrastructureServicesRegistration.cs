using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Infrastructure.Mail;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            //IF NOT WORKED
            //Microsoft.Extensions.Configuration.Binder
            //services.Configure<EmailSettings>(x => configuration.GetSection("EmailSettings").Bind(x));

            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
        //public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        //    services.AddTransient<IEmailSender, EmailSender>();
        //    return services;
        //}
    }
}