using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using WsAuth;

namespace EmployeeTempTracker.Controllers {
    
    class HomeControllerLogic : Controller {
        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            ViewData["Title"] = "Home";
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard(LoginModel lm) {
            if (!lm.SessionValid) return RedirectToAction("Index", "Login");
            ViewData["DomainName"] = lm.DomainName;
            return View("Dashboard");
        }

        
        // GET https://capstone.ohitski.org/Home/Privacy
        public IActionResult Privacy() {
            return View("Privacy");
        }

        // GET https://capstone.ohitski.org/Home/Error
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}