using System;
using Microsoft.Extensions.DependencyInjection;
using di_prac.Controllers;
using di_prac.Services;
using di_prac.Repositories;
using di_prac.Data;


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
            CreateDb(serviceProvider);

            // Run app
            var controller = serviceProvider.GetService<HomeController>();

            controller.Run();
  
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add services
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            // Add controllers
            services.AddTransient<HomeController>();

            // Add db context
            services.AddDbContext<MyAppContext>();
        }

        private static void CreateDb(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyAppContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}