using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            return viewProcessor_.EnterScreening();
        }

        // GET https://capstone.ohitski.org/Screening/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string org, string temperature, string symptoms, string closeContact, string intlTravel) {
            return viewProcessor_.ProcessScreening(fname, lname, id, org, temperature, symptoms, closeContact, intlTravel);
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