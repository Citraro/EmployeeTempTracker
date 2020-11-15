using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTempTracker.Controllers {
    public class LoginController : Controller {
        IntellineticsApi api_ = new IntellineticsApi();

        private LoginControllerLogic viewProcessor_ = new LoginControllerLogic();

        // GET https://capstone.ohitski.org/Login
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        [HttpPost]
        public async Task<IActionResult> AuthUser(string domain, string uname, string passwd)
        {
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
                return RedirectToAction("Dashboard", "Home");
            }
            return RedirectToAction("InvalidLogin");
        }
        /*public async Task<IActionResult> AuthUser(string domain, string uname, string passwd) {
            return await viewProcessor_.AuthUser(domain, uname, passwd);
        }*/

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }
    }
}