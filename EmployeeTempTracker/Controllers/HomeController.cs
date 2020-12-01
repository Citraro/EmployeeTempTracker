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
            string domain = Request.Cookies["DomainName"];
            log.Info("Index accessed");
            return viewProcessor_.Dashboard(domain);
        }
        [Authorize]
        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
            string domain = Request.Cookies["DomainName"];
            string sessionId = Request.Cookies["SessionId"];
            return viewProcessor_.Dashboard(domain,sessionId);
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
        
        public IActionResult Analytics(int days, int id, string session,int appId) {
            return viewProcessor_.Analytics(days, id,session,appId);
        }

        // GET https://capstone.ohitski.org/Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return viewProcessor_.Error();
        }

    }
}
