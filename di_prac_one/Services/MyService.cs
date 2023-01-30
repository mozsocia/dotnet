using di_prac.Services;
using System;
namespace di_prac
{
    public class MyService : IMyService
    {
        public void DoWork()
        {
            // Implementation of the service
            Console.WriteLine("Hello World!");
        }
    }
}
