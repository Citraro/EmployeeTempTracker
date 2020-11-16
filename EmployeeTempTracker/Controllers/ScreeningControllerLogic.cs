using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;

namespace EmployeeTempTracker.Controllers {
    class ScreeningControllerLogic : Controller {
        LoginController login = new LoginController();

        // GET https://capstone.ohitski.org/Screening
        public IActionResult Index() {
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");
            
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Screening/EnterScreening
        public IActionResult EnterScreening(String domain) {
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");
            ViewData["Title"] = "Health Screening";
            ViewData["DomainName"] = domain;

            if(domain == "training1"){
                return View("GSIEnterScreening");
            }else if(domain == "training4"){
                return View("INTEnterScreening");} 
            else{
                return RedirectToAction("Home","Dashboard");
            }
        }

        // GET https://capstone.ohitski.org/Screening/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, int id, string org, string temperature, string highTemp, string symptoms, string closeContact, string intlTravel,string Sig, string sigPrintName, DateTime sigDate) {
            // Takes EnterScreening form data and creates a ScreeningModel object from it.
            // Maybe have a popup that makes the signee verify everything is true?
            
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            ScreeningModel screening = new ScreeningModel();
            screening.EmpId = id;
            screening.Temp = temperature;
            screening.HighTemp = highTemp;
            screening.Symptoms = symptoms;
            screening.CloseContact = closeContact;
            screening.IntlTravel = intlTravel;
            screening.Date = DateTime.Now;       
            screening.Sig = Sig;
            screening.SigPrintName = sigPrintName;
            screening.SigDate = sigDate;
            ViewData["Screening"] = screening;
            // STILL NEED Time

            // Check for questionairre anomalies
            bool flag = false;
            if (screening.Symptoms == "Yes")        flag = true;
            if (screening.CloseContact == "Yes")    flag = true;
            if (screening.IntlTravel == "Yes")      flag = true;
            if (screening.Sig == "No") flag = true;

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
        public IActionResult ReviewScreening(ScreeningModel screening) { 
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");

            ViewData["Title"] = "Review Screening";
            ViewData["Screening"] = screening; 
            return View("ReviewScreening"); 
        }

        // POST https://capstone.ohitski.org/Screening/Edit
        public IActionResult Edit(ScreeningModel updatedScreening) {//TODO: instead of screening param here, pass Emp.Id
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