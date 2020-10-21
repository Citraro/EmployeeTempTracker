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
    public class LoginController : Controller {

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger) {
            _logger = logger;
        }

        // Get http://capstone.ohitski.org/Login
        public IActionResult Index(){
            // Returns Views/Login/Index.cshtml
            ViewData["Title"] = "Login";
            return View();
        }

        // Example of adding a route.
        // Changing this to return an IActionResult (return View();) would search for /Views/Login/AuthUser.cshtml
        // Get http://capstone.ohitski.org/Login/AuthUser
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
                return RedirectToAction("UserDashboard",new{uname = uname,passwd = passwd, id = id});
            }
            else{
                return RedirectToAction("InvalidLogin");
            }
        }

        public IActionResult UserDashboard(string uname, string passwd, int id = 1){
            ViewData["Title"] = "UserDashboard";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Hello, {uname}, your id is {id} and you entered {passwd} as your password.");
            return View();
        }

        public IActionResult InvalidLogin(){
            ViewData["Title"] = "InvalidLogin";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Invalid username/password.");
            return View();
        }


    }
}