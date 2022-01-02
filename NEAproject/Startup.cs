using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NEAproject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEAproject.Services;
using NEAproject.Models;

namespace NEAproject
{
    public class Startup
    {
        //class for configuration and registering services 
        public Startup(IConfiguration configuration)
        { //parameterised constructor with configuration object 
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //actually hold all values for config of web app 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //registering db context 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
               //registering identity option for authentication functionality 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDbContext<NEAdbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            //registering controller with views service
            services.AddRazorPages();
            //adding functionality for razor pages 
            //registering bespoke service - middleware
            services.AddScoped<IViewRenderService, ViewRenderService>();
            //to register the object throughout the program and that the state of the object should persist
            //IViewRenderService is the interface, ViewRenderService is the object that we need to persist
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                //if it is in local development mode en it will use exception and error pages which will be useful for developer
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //if it is not in local development then , errors will be shown on error page of home folder

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //generally in asp core net it doesnt allow any of the files to be accessed outside of wwwroot folder we need to regsiter the static files if we are going to
            //access any files that are not in wwwroot by passing the path of that bespoke folder while registering services for use static files

            app.UseRouting();
            //register routing services

            app.UseAuthentication();
            //to allow the application to use authentications 
            app.UseAuthorization();
            //allows application to use authorisation
            //registering the routing for mvc style 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
