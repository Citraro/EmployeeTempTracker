using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTempTracker.Controllers {
    public class LoginController : Controller {

        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger) {
            _logger = logger;
        }
        private LoginControllerLogic viewProcessor_ = new LoginControllerLogic();

        // GET https://capstone.ohitski.org/Login
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        [HttpPost]
        public IActionResult AuthUser(string uname, string passwd, int id = 1) { // TODO: Move functionality to Login()?
            return viewProcessor_.AuthUser(uname, passwd, id);
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }

        // GET https://capstone.ohitski.org/Login/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string DomainName, bool? SessionValid, string Username) {
            return viewProcessor_.Login(DomainName, SessionValid, Username);
        }

        // POST https://capstone.ohitski.org/Login/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel lm) {
            return viewProcessor_.Login(lm);
        }
    }
}