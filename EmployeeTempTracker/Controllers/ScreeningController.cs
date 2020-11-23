using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace EmployeeTempTracker.Controllers {
    [Authorize]
    public class ScreeningController : Controller {
 
        private ScreeningControllerLogic viewProcessor_ = new ScreeningControllerLogic();

        // GET https://capstone.ohitski.org/Screening
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // GET https://capstone.ohitski.org/Screening/EnterScreening
        public IActionResult EnterScreening() {
            string domain = Request.Cookies["DomainName"];
            return viewProcessor_.EnterScreening(domain);
        }

        // POST https://capstone.ohitski.org/Screening/ProcessScreening
        [HttpPost]
        public IActionResult ProcessScreening(string fname, string lname, int id, 
            string temperature, string highTemp, string symptoms, string closeContact, 
            string intlTravel,string Sig, string sigPrintName, DateTime sigDate) {

            string sessionId = Request.Cookies["SessionId"];
            string domain = Request.Cookies["DomainName"];
            return viewProcessor_.ProcessScreening(fname, lname, id, temperature, highTemp, symptoms, closeContact, intlTravel, Sig, sigPrintName, sigDate, sessionId, domain);
        }

        // GET https://capstone.ohitski.org/Screening/SendHome
        public IActionResult SendHome(ScreeningModel screening) {
            return viewProcessor_.SendHome(screening);
        }

        // GET https://capstone.ohitski.org/Screening/ReviewScreening
        public IActionResult ReviewScreening(ScreeningModel screening) { 
            return viewProcessor_.ReviewScreening(screening);
        }

        // POST https://capstone.ohitski.org/Screening/Edit
        [HttpPost]
        public IActionResult Edit(ScreeningModel screening) {//TODO: instead of screening param here, pass Emp.Id
            return viewProcessor_.Edit(screening);
        }

    }
}