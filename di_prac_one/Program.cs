using System;
using Microsoft.Extensions.DependencyInjection;
using di_prac.Services;

namespace di_prac
{
    class Program
    {
        static void Main(string[] args)
        {
             var services = new ServiceCollection();
            services.AddTransient<IMyService, MyService>();

            var serviceProvider = services.BuildServiceProvider();

            var myService = serviceProvider.GetService<IMyService>();
            myService.DoWork();
        }
    }
}
