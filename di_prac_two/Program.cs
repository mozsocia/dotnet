using System;
using Microsoft.Extensions.DependencyInjection;
using di_prac.Controllers;
using di_prac.Services;
using di_prac.Repositories;

namespace di_prac
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Create service provider
            var serviceProvider = services.BuildServiceProvider();

            // Run app
            serviceProvider.GetService<HomeController>().Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add services
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            // Add controllers
            services.AddTransient<HomeController>();
        }
    }
}