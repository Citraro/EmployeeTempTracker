using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;

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

        // GET https://capstone.ohitski.org/Login/AuthUser
        public IActionResult AuthUser(string uname, string passwd, int id = 1) {
            return viewProcessor_.AuthUser(uname, passwd, id);
        }

        // GET https://capstone.ohitski.org/Login/UserDashboard
        public IActionResult DashBoard(string uname, string passwd, int id = 1) {
            return viewProcessor_.DashBoard(uname, passwd, id);
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }

    }
}