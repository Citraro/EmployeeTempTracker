using Microsoft.AspNetCore.Authorization;
using EmployeeTempTracker.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeTempTracker.Controllers
{
    public class HomeController : Controller {

        private HomeControllerLogic viewProcessor_ = new HomeControllerLogic();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            log.Info("Index accessed"); //test log
            return viewProcessor_.Index();
        }
        [Authorize]
        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
            string domain = Request.Cookies["DomainName"];
            return viewProcessor_.Dashboard(domain);
        }

        [HttpPost]
        // GET https://capstone.ohitski.org/Home/Dashboard
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("SessionId");
            Response.Cookies.Delete("DomainName");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Login");
        }
        
        public IActionResult Analytics(int days = 7, string id = null) {
            return viewProcessor_.Analytics(days, id);
        }

        // GET https://capstone.ohitski.org/Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return viewProcessor_.Error();
        }
        // GET https://capstone.ohitski.org/Home/DashboardTemp
        public IActionResult DashboardTemp(){
            return viewProcessor_.DashboardTemp();
        }
    }
}
