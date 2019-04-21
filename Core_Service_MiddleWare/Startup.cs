using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core_Service_MiddleWare.Data;
using Core_Service_MiddleWare.Models;
using Core_Service_MiddleWare.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Core_Service_MiddleWare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //服务注册
            services.AddSingleton<IWelcomService, WelcomService>();

            services.AddSingleton<IRepository<Student>, InMemoryRepository>();
            services.AddScoped<IRepository<Student>, EFCoreRepository>();

            //add-migration InitialIdentity -Context IdentityDbContext
            // update-database -Context IdentityDbContext
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Core_Service_MiddleWare"));
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<IdentityDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
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
            app.UseStaticFiles();//wwwroot文件夹
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath + "/node_modules")),
            });
            app.UseAuthentication();
            app.UseMvc(builder =>
            {
                builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            //中间件
            //app.Use(next =>
            //{
            //    logger.LogInformation("Use中间件...");
            //    return async httpContext =>
            //    {
            //        logger.LogInformation("中间件里判断...");
            //        if (httpContext.Request.Path.StartsWithSegments("/test"))
            //            await httpContext.Response.WriteAsync("TestMiddleWare");
            //        else
            //            await next(httpContext);
                    
            //    };
            //});
            //app.UseWelcomePage(new WelcomePageOptions { Path = "/welcome" });
            app.UseFileServer();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(welcomService.WelcomeMessage());
            //});
        }
    }
}
