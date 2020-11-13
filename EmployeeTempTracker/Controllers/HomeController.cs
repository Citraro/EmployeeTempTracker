using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace EmployeeTempTracker.Controllers
{
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        private HomeControllerLogic viewProcessor_ = new HomeControllerLogic();

        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
            return viewProcessor_.Dashboard();
        }

        public IActionResult Analytics(int days = 7, string id = null) {
            return viewProcessor_.Analytics(days, id);
        }
        
        // GET https://capstone.ohitski.org/Home/Privacy
        public IActionResult Privacy() {
            return viewProcessor_.Privacy();
        }

        // GET https://capstone.ohitski.org/Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return viewProcessor_.Error();
        }
    }
}
