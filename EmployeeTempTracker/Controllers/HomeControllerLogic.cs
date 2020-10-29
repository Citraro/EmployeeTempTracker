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

namespace EmployeeTempTracker.Controllers {
    
    class HomeControllerLogic : Controller {
        public IActionResult Index() {
            ViewData["Title"] = "Home";
            return View("Index");
        }

        public ActionResult Login(string DomainName, bool? SessionValid, string Username) {
            LoginModel mod = new LoginModel();
            mod.DomainName = DomainName;
            if (SessionValid.HasValue)
            {
                mod.SessionValid = SessionValid.Value;
                if (!SessionValid.Value)
                {
                    TempData["UserMessageErrorHeader"] = "Error";
                    TempData["UserMessageErrorBody"] = "Invalid Session.  Please login.";
                }
            }
            mod.Username = Username;
            
            return View(mod);
        }

        public IActionResult Login(LoginModel lm)
        {
            int callerID = 117;
            if (ModelState.IsValid)
            {
                //LoginModel am = new LoginModel();
                AuthorizationModel am = new AuthorizationModel();
                WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration ec = WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration.GXPAuthenticationSoap;
                WsAuth.GXPAuthenticationSoapClient svc = new WsAuth.GXPAuthenticationSoapClient(ec);
                WsAuth.LoginResult res = new WsAuth.LoginResult();
                res = svc.Login(lm.DomainName, lm.Username, lm.Password, callerID);
                //res.LoginSessionID;

                if (String.IsNullOrEmpty(res.LoginSessionID))
                {
                    TempData["UserMessageErrorHeader"] = "Error";
                    if (res.ServiceError != null)
                    {
                        TempData["UserMessageErrorBody"] = res.ServiceError.Message;
                    }
                    else
                    {
                        TempData["UserMessageErrorBody"] = "Login Failure";
                    }
                    // FormsAuthentication.SignOut();                   // does not exist
                    return View("Login", lm);
                }
                else
                {
                    am.SessionID = res.LoginSessionID;
                    am.UserName = lm.Username;
                    am.Domain = lm.DomainName;
                    return View("Login", lm);
                }
            }
            else
            {
                return View("Login", lm);
            }
        }

        public IActionResult EnterScreening() {
            ViewData["Title"] = "Health Screening";
            return View("EnterScreening");
        }

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

            // Check for questionairre anomalies
            bool flag = false;
            if (screening.Symptoms == "Yes")        flag = true;
            if (screening.CloseContact == "Yes")    flag = true;
            if (screening.IntlTravel == "Yes")      flag = true;

            //if (flag) return RedirectToAction("SendHome", "Home", new {screening});
            //else return View();

            //TODO: SOME LOGIC TO ADD SCREENING TO DATABASE

            return RedirectToAction("ReviewScreening", screening); //pass screening EmpId after adding to db instead of passing screening
        }
        
        public IActionResult ReviewScreening(ScreeningModel screening){ //TODO: instead of screening param here, pass Emp.Id
            ViewData["Title"] = "Review Screening";
            ViewData["Screening"] = screening; //instead of using screening as param, search db via API for screening matching Emp.Id
        
            return View(); //pass screening to here
        }

        public IActionResult Edit(ScreeningModel updatedScreening){
            //update screening in DB using EntityFramework in real-life application
            
            //update list by removing old screening and adding new
            // var oldScreening = screeningList.Where(s => s.EmpId == updatedScreening.EmpId).FirstOrDefault();
            // screeningList.Remove(oldScreening);
            // screeningList.Add(updatedScreening);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy() {
            return View("Privacy");
        }
    
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}