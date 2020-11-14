using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers {
    class LoginControllerLogic : Controller {
        IntellineticsApi api_ = new IntellineticsApi();
        
        // GET https://capstone.ohitski.org/Login
        public IActionResult Index(){
            ViewData["Title"] = "Login";
            return View("Index");
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        public IActionResult AuthUser(string domain, string uname, string passwd) {
            ViewData["Title"] = "Authenticate";
            LoginModel authenticated = api_.CheckUserLogin(new LoginModel(domain, uname, passwd));            
            if(authenticated.SessionValid) return RedirectToAction("DashBoard", "Home", authenticated);
            else return RedirectToAction("InvalidLogin");
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            ViewData["Title"] = "InvalidLogin";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Invalid username or password.");
            return View("InvalidLogin");
        }
    }
}