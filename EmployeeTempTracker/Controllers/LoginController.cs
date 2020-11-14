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
        public IActionResult AuthUser(string domain, string uname, string passwd) {
            return viewProcessor_.AuthUser(domain, uname, passwd);
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }
    }
}