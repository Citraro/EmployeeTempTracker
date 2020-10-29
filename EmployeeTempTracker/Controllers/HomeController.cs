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

        // get https://capstone.ohitski.org/Home
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // get https://capstone.ohitski.org/Home/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string DomainName, bool? SessionValid, string Username) {
            return viewProcessor_.Login(DomainName, SessionValid, Username);
        }

        // post https://capstone.ohitski.org/Home/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel lm) {
            return viewProcessor_.Login(lm);
        }
        
        // Get https://capstone.ohitski.org/Home/EnterScreening
        public IActionResult EnterScreening() {
            return viewProcessor_.EnterScreening();
        }

        // Get https://capstone.ohitski.org/Home/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string org, string temperature, string symptoms, string closeContact, string intlTravel) {
            return viewProcessor_.ProcessScreening(fname, lname, id, org, temperature, symptoms, closeContact, intlTravel);
        }

        // get https://capstone.ohitski.org/Home/ReviewScreening
        public IActionResult ReviewScreening(ScreeningModel screening) { //TODO: instead of screening param here, pass Emp.Id
            return viewProcessor_.ReviewScreening(screening);
        }

        // post https://capstone.ohitski.org/Home/Edit
        [HttpPost]
        public IActionResult Edit(ScreeningModel updatedScreening) {
            return viewProcessor_.Edit(updatedScreening);
        }

        // get https://capstone.ohitski.org/Home/Privacy
        public IActionResult Privacy() {
            return viewProcessor_.Privacy();
        }

        // get https://capstone.ohitski.org/Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return viewProcessor_.Error();
        }
    }
}
