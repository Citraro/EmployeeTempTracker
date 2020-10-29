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

        // GET https://capstone.ohitski.org/Home/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string DomainName, bool? SessionValid, string Username) {
            return viewProcessor_.Login(DomainName, SessionValid, Username);
        }

        // POST https://capstone.ohitski.org/Home/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel lm) {
            return viewProcessor_.Login(lm);
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard() {
            return viewProcessor_.Dashboard();
        }
        
        // GET https://capstone.ohitski.org/Home/EnterScreening
        public IActionResult EnterScreening() {
            return viewProcessor_.EnterScreening();
        }

        // Get https://capstone.ohitski.org/Home/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string org, string temperature, string symptoms, string closeContact, string intlTravel) {
            return viewProcessor_.ProcessScreening(fname, lname, id, org, temperature, symptoms, closeContact, intlTravel);
        }

        // GET https://capstone.ohitski.org/Home/SendHome
        public IActionResult SendHome(ScreeningModel screening) {
            return viewProcessor_.SendHome(screening);
        }

        // GET https://capstone.ohitski.org/Home/ReviewScreening
        public IActionResult ReviewScreening(ScreeningModel screening) { //TODO: instead of screening param here, pass Emp.Id
            return viewProcessor_.ReviewScreening(screening);
        }

        // POST https://capstone.ohitski.org/Home/Edit
        [HttpPost]
        public IActionResult Edit(ScreeningModel updatedScreening) {
            return viewProcessor_.Edit(updatedScreening);
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
