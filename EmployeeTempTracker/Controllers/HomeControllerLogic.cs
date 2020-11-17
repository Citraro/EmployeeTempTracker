using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;
using Newtonsoft.Json;

namespace EmployeeTempTracker.Controllers {
    
    class HomeControllerLogic : Controller {
        private static IntellineticsApi api_ = new IntellineticsApi();

        // GET https://capstone.ohitski.org/Home
        public IActionResult Index() {
            ViewData["Title"] = "Home";
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard(string domain = null) {
            ViewData["DomainName"] = domain;
            return View("Dashboard");
        }

        // ToDo: Remove?
        // public IActionResult Dashboard(LoginModel lm) {
        //     if (!lm.SessionValid) return RedirectToAction("Index", "Login");
        //     ViewData["DomainName"] = lm.DomainName;
        //     return View("Dashboard");
        // }

        public IActionResult Analytics(int numDays, string id) {
            ScreeningModel [] graphData = api_.FetchUserScreeningsByDay(numDays);

            // Start computing statistics
            double maxTemp = -1.0, minTemp = 200.0, sum = 0.0;
            foreach (ScreeningModel obj in graphData) {
                double current = Convert.ToDouble(obj.Temp);
                maxTemp = (maxTemp < current) ? current : maxTemp;
                minTemp = (minTemp > current) ? current : minTemp;
                sum += current;
            }
            double average = sum / numDays;
            double rangeHalf = (maxTemp - minTemp) / 2;

            // Pass data to the view
            ViewData["Id"] = graphData[0].EmpId;
            ViewData["ScreeningCount"] = numDays;
            ViewData["Average"] = average.ToString("F2");
            ViewData["MinTemp"] = minTemp.ToString("F2");
            ViewData["MaxTemp"] = maxTemp.ToString("F2");
            ViewData["ExpectedRange"] = $"{(average - rangeHalf).ToString("F2")} - {(average + rangeHalf).ToString("F2")}";
            ViewData["Screenings"] = JsonConvert.SerializeObject(graphData);
            return View();
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