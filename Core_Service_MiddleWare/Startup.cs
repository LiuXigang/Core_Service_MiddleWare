using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Models;
using Core_Service_MiddleWare.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core_Service_MiddleWare
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //服务注册
            services.AddSingleton<IWelcomService, WelcomService>();

            services.AddScoped<IRepository<Student>, InMemoryRepository>();
        }
        
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IWelcomService welcomService,
            ILogger<Startup> logger) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseStaticFiles();
            app.UseMvc(builder =>
            {
                builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            //中间件
            app.Use(next =>
            {
                logger.LogInformation("Use中间件...");
                return async httpContext =>
                {
                    logger.LogInformation("中间件里判断...");
                    if (httpContext.Request.Path.StartsWithSegments("/test"))
                        await httpContext.Response.WriteAsync("TestMiddleWare");
                    else
                        await next(httpContext);
                    
                };
            });
            app.UseWelcomePage(new WelcomePageOptions { Path = "/welcome" });
            app.UseFileServer();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(welcomService.WelcomeMessage());
            });
        }
    }
}
