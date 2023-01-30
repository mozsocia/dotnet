using di_prac.Services;
using System;

namespace di_prac.Controllers
{
    public class HomeController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public void Run()
        {
            var message = _homeService.GetWelcomeMessage();
            Console.WriteLine(message);
        }
    }
}
