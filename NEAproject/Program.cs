using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject

    //main program that will trigger the application 
{
    public class Program
    {
        public static void Main(string[] args)
            //main entry point 
        {
            CreateHostBuilder(args).Build().Run();
            //build the program as well as run the web app 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //it will trigger the startup class as we have registered the services and configuration in the startup.cs
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
