using di_prac.Repositories;

namespace di_prac.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public string GetWelcomeMessage()
        {
            return _homeRepository.GetWelcomeMessage();
        }
    }
}
