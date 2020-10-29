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
    public class LoginControllerLogic : Controller {
        public IActionResult Index(){
            ViewData["Title"] = "Login";
            return View("Index");
        }

        public IActionResult AuthUser(string uname, string passwd, int id = 1) {
            ViewData["Title"] = "Authenticate";
            // Handle user login API call here, unless it will be implemented in APIController.cs
            
            //Will use something like below code when we have API/login calls available to compare input username/password to id database:

            // var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();  
            // if (obj != null){  
            //     Session["UserID"] = obj.UserId.ToString();  
            //     Session["UserName"] = obj.UserName.ToString();  
            //     return RedirectToAction("UserDashBoard");  
            // } 

            Boolean someLogicToCheckDataBaseForUser = true; //TEMPORARY FIELD, REMOVE WHEN API CALLS ARE IMPLEMENTED
            
            if(someLogicToCheckDataBaseForUser){
                // Redirect to http://capstone.ohitski.org/Login/Dashboard
                return RedirectToAction("DashBoard", "Home", new {uname = uname, passwd = passwd, id = id});

                // Example of how to redirect to another controller (http://capstone.ohitski.org/Home/EnterScreening):
                // return RedirectToAction("EnterScreening", "Home");
            }
            else{
                // Redirect to http://capstone.ohitski.org/Login/InvalidLogin
                return RedirectToAction("InvalidLogin");
            }
        }

        public IActionResult InvalidLogin(){
            ViewData["Title"] = "InvalidLogin";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Invalid username/password.");
            return View("InvalidLogin");
        }
    }
}