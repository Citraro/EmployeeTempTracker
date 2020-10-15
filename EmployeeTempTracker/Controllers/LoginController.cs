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

        // Get http://capstone.ohitski.org/Login
        public IActionResult Index(){
            // Returns Views/Login/Index.cshtml
            return View();
        }

        // Example of adding a route.
        // Changing this to return an IActionResult (return View();) would search for /Views/Login/AuthUser.cshtml
        // Get http://capstone.ohitski.org/Login/AuthUser
        public string AuthUser(string uname, string passwd, int id) {
            return HtmlEncoder.Default.Encode($"Hello {uname}, your Id is {id} and you entered {passwd} to login.");
        }

    }
}