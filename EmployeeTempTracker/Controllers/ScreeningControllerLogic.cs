using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;

namespace EmployeeTempTracker.Controllers {
    class ScreeningControllerLogic : Controller {
        // GET https://capstone.ohitski.org/Screening
        public IActionResult Index() {
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");
            
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Screening/EnterScreening
        public IActionResult EnterScreening() {
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");
            
            ViewData["Title"] = "Health Screening";
            return View("EnterScreening"); 
        }

        // GET https://capstone.ohitski.org/Screening/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string org, string temperature, string symptoms, string closeContact, string intlTravel) {
            // Takes EnterScreening form data and creates a ScreeningModel object from it.
            // Maybe have a popup that makes the signee verify everything is true?
            
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            ScreeningModel screening = new ScreeningModel();
            screening.EmpId = id;
            screening.Temp = temperature;
            screening.Symptoms = symptoms;
            screening.CloseContact = closeContact;
            screening.IntlTravel = intlTravel;
            screening.Date = DateTime.Now;
            ViewData["Screening"] = screening;
            // STILL NEED SigPrintName, Time, SigDate

            // Check for questionairre anomalies
            bool flag = false;
            if (screening.Symptoms == "Yes")        flag = true;
            if (screening.CloseContact == "Yes")    flag = true;
            if (screening.IntlTravel == "Yes")      flag = true;

            if (flag) return RedirectToAction("SendHome", new {screening});
            //TODO: SOME LOGIC TO ADD SCREENING TO DATABASE
            else return RedirectToAction("ReviewScreening", screening); //pass screening EmpId after adding to db instead of passing screening
        }

        // GET https://capstone.ohitski.org/Screening/SendHome
        public IActionResult SendHome(ScreeningModel screening) {
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");
            
            ViewData["Message"] = "You answered 'yes' to one or more questions on the questionairre. For the health and safety of us all, you are required to go home.";
            return View("SendHome");
        }

        // GET https://capstone.ohitski.org/Screening/ReviewScreening
        public IActionResult ReviewScreening(ScreeningModel screening) { //TODO: instead of screening param here, pass Emp.Id
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");

            ViewData["Title"] = "Review Screening";
            ViewData["Screening"] = screening; //instead of using screening as param, search db via API for screening matching Emp.Id
            return View("ReviewScreening"); //pass screening to here
        }

        // POST https://capstone.ohitski.org/Screening/Edit
        public IActionResult Edit(ScreeningModel updatedScreening) {
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");
            //update screening in DB using EntityFramework in real-life application
            
            //update list by removing old screening and adding new
            // var oldScreening = screeningList.Where(s => s.EmpId == updatedScreening.EmpId).FirstOrDefault();
            // screeningList.Remove(oldScreening);
            // screeningList.Add(updatedScreening);

            return RedirectToAction("Index");
        }

    }
}