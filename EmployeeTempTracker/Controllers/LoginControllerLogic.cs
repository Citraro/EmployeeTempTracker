using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers {
    class LoginControllerLogic : Controller {
        //IntellineticsApi api_ = new IntellineticsApi();
        
        // GET https://capstone.ohitski.org/Login
        public IActionResult Index(){
            ViewData["Title"] = "Login";
            return View("Index");
        }

 /*       // POST https://capstone.ohitski.org/Login/AuthUser
        public async Task<IActionResult> AuthUser(string domain, string uname, string passwd) {
            ViewData["Title"] = "Authenticate";
            LoginModel authenticated = api_.CheckUserLogin(new LoginModel(domain, uname, passwd));
            if (authenticated.SessionValid)
            {
                var claims = new List<Claim>
                {
                    new Claim("SessionId", authenticated.SessionId),
                    new Claim(ClaimTypes.Name, authenticated.Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                //return RedirectToAction(ReturnUrl == null ? "/home/dashboard" : ReturnUrl);
                return RedirectToAction("Dashboard", "Home");
            }
            return RedirectToAction("InvalidLogin");
        }*/

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            ViewData["Title"] = "InvalidLogin";
            ViewData["Message"] = HtmlEncoder.Default.Encode($"Invalid username or password.");
            return View("InvalidLogin");
        }

    }
}