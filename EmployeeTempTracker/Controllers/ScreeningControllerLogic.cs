using Microsoft.AspNetCore.Mvc;

namespace EmployeeTempTracker.Controllers {
    class ScreeningControllerLogic : Controller {
        // GET https://capstone.ohitski.org/Screening
        public IActionResult Index() {
            bool authenticated = true;
            if (authenticated) return View("Index");
            else return RedirectToAction("Index", "Login");
        }
    }
}