 using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
 using System.Reflection;
 using System.Threading.Tasks;
 using HR.LeaveManagement.MVC.Contracts;
 using HR.LeaveManagement.MVC.Middleware;
 using HR.LeaveManagement.MVC.Services;
 using HR.LeaveManagement.MVC.Services.Base;
 using Microsoft.AspNetCore.Authentication.Cookies;
 using Microsoft.AspNetCore.Http;

 namespace HR.LeaveManagement.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.Configure<CookiePolicyOptions>(x =>
            {
                x.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = new PathString("/users/login");
            });
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddHttpClient<IClient, Client>(x => x.BaseAddress = new Uri("https://localhost:44348"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<ILeaveTypeService, LeaveTypeService>();
            services.AddSingleton<ILeaveRequestService, LeaveRequestService>();
            services.AddSingleton<ILeaveAllocationService, LeaveAllocationService>();

            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMiddleware<RequestMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
