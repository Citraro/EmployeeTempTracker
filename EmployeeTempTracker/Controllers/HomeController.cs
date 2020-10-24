using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Get http://capstone.ohitski.org/
        public IActionResult Index()
        {
            return View();
        }

        // Get http://capstone.ohitski.org/Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // Get http://capstone.ohitski.org/Home/EnterScreening
        public IActionResult EnterScreening() {
            ViewData["Title"] = "Health Screening";
            return View();
        }

        // Get http://capstone.ohitski.org/Home/EnterScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string org, string temperature, string symptoms, string closeContact, string intlTravel) {
            // Takes EnterScreening form data and creates a ScreeningModel object from it.
            // Maybe have a popup that makes the signee verify everything is true?
            
            ScreeningModel screening = new ScreeningModel();
            screening.EmpId = id;
            screening.Temp = temperature;
            screening.Symptoms = symptoms;
            screening.CloseContact = closeContact;
            screening.IntlTravel = intlTravel;
            screening.Date = DateTime.Now;
            ViewData["Screening"] = screening;

            // STILL NEED SigPrintName, Time, SigDate
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
