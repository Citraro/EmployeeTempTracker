using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;

namespace EmployeeTempTracker.Controllers {
    class ScreeningControllerLogic : Controller {

        private IntellineticsApi api_ = new IntellineticsApi();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        LoginController login = new LoginController();

        // Placeholder
        public IActionResult Index() {
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Screening/EnterScreening
        public IActionResult EnterScreening(string domain, string id = null, string fname = null, string lname = null) {
            ViewData["Title"] = "Health Screening";
            ViewData["DomainName"] = domain;
            ViewData["EmployeeId"] = id;
            ViewData["FirstName"] = fname;
            ViewData["LastName"] = lname;
            if(domain == "training1"){
                return View("GSIEnterScreening");
            }else if(domain == "training4"){
                return View("INTEnterScreening");} 
            else{
                return RedirectToAction("Dashboard","Home");
            }
        }

        // POST https://capstone.ohitski.org/Screening/ProcessScreening
        public IActionResult ProcessScreening(string fname, string lname, string id, string temperature, string highTemp, string symptoms, string closeContact, string intlTravel,string Sig, string sigPrintName, DateTime sigDate, string sessionId, string domain) {

            // Ensure proper mask for temperature object
            double deg = Convert.ToDouble(temperature);
            temperature = deg.ToString("F2");
            while (temperature.Length < 6) temperature = temperature.Insert(0, "0");
            if (temperature.Length > 6) temperature = temperature.Substring(temperature.Length - 5);

            ScreeningModel screening = new ScreeningModel();
            screening.FirstName = fname;
            screening.LastName = lname;
            screening.EmpId = id;
            screening.Temp = temperature;
            screening.HighTemp = highTemp;
            screening.Symptoms = symptoms;
            screening.CloseContact = closeContact;
            screening.IntlTravel = intlTravel;
            screening.Date = DateTime.Now;       
            screening.Time = DateTime.Now;
            screening.Sig = Sig;
            screening.SigPrintName = sigPrintName;
            screening.SigDate = DateTime.Now;
            ViewData["Screening"] = screening;

            // Check for questionairre anomalies
            bool flag = false;
            if (screening.Symptoms == "Yes")        flag = true;
            if (screening.CloseContact == "Yes")    flag = true;
            if (screening.IntlTravel == "Yes")      flag = true;
            if (screening.Sig == "No")              flag = true;
            if (screening.HighTemp == "Yes")        flag = true;
            if (Convert.ToDouble(screening.Temp) > 100.4) flag = true;

            int appId = (domain == "training1") ? 116 : 216; // GSI : Intellinetics DAILY_HEALTH_RECORD app ids
            WsCore.FMResult res = null;
            try{
                res = api_.InsertScreening(screening, domain, sessionId, appId);
            }
            catch(Exception e){
                log.Debug(e.Message);
            }
            if (res.ResultCode == -1) return RedirectToAction("Dashboard", "Home");
            else if (flag) return RedirectToAction("SendHome", screening);
            else return RedirectToAction("ReviewScreening", screening); //pass screening EmpId after adding to db instead of passing screening
        }

        // GET https://capstone.ohitski.org/Screening/SendHome
        public IActionResult SendHome(ScreeningModel screening) {
            ViewData["IntlTravel"]      = !(screening.IntlTravel == "Yes");
            ViewData["CloseContact"]    = !(screening.CloseContact == "Yes");
            ViewData["Symptoms"]        = !(screening.Symptoms == "Yes");
            ViewData["HighTemp"]        = !(Convert.ToDouble(screening.Temp) > 100.4 || screening.HighTemp == "Yes");
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
            //update screening in DB using EntityFramework in real-life application
            
            //update list by removing old screening and adding new
            // var oldScreening = screeningList.Where(s => s.EmpId == updatedScreening.EmpId).FirstOrDefault();
            // screeningList.Remove(oldScreening);
            // screeningList.Add(updatedScreening);

            return RedirectToAction("Index");
        }

    }
}