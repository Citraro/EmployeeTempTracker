using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers {
    class LoginControllerLogic : Controller {
        
        // GET https://capstone.ohitski.org/Login
        public IActionResult Index(){
            ViewData["Title"] = "Login";
            return View("Index");
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        public IActionResult AuthUser(string uname, string passwd, int id = 1) {
            ViewData["Title"] = "Authenticate";
            // Handle user login API call here, unless it will be implemented in APIController.cs
            
            //Will use something like below code when we have API/login calls available to compare input username/password to id database:

            // var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();  
            // if (obj != null){  
            //     Session["UserID"] = obj.UserId.ToString();  
            //     Session["UserName"] = obj.UserName.ToString();  
            //     return RedirectToAction("Dashboard", "Home");  
            // } 

            Boolean someLogicToCheckDataBaseForUser = true; //TEMPORARY FIELD, REMOVE WHEN API CALLS ARE IMPLEMENTED
            
            if(someLogicToCheckDataBaseForUser) {
                return RedirectToAction("DashBoard", "Home", new {uname = uname});
            }
            else {
                return RedirectToAction("InvalidLogin");
            }
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            ViewData["Title"] = "InvalidLogin";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Invalid username or password.");
            return View("InvalidLogin");
        }
    }
}