using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers {
    
    class HomeControllerLogic : Controller {
        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            ViewData["Title"] = "Home";
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
        //    if (!lm.SessionValid) return RedirectToAction("Index", "Login");
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