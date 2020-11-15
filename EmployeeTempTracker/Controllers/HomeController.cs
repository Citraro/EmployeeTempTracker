
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers
{
    public class HomeController : Controller {

        private HomeControllerLogic viewProcessor_ = new HomeControllerLogic();

        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            return viewProcessor_.Index();
        }
        [Authorize]
        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
            return viewProcessor_.Dashboard();
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
