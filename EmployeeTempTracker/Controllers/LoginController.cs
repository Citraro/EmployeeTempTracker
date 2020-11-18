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
        // (Amy): Couldnt get this to work in the logic controller so leaving it here for now
        // api call to athenticate user and if authenticated, cookie is created
        [HttpPost]
        public async Task<IActionResult> AuthUser(string domain, string uname, string passwd) {
            ViewData["Title"] = "Authenticate";
            var appID = domain == "training1" ? 117 : 216; //TODO: refactor later
            LoginModel authenticated = api_.CheckUserLogin(new LoginModel(domain, uname, passwd), appID);
            if (authenticated.SessionValid) {
                var claims = new List<Claim>
                {
                    new Claim("SessionId", authenticated.SessionId),
                    new Claim(ClaimTypes.Name, authenticated.Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Dashboard", "Home", new {domain = authenticated.DomainName});
            }
            return RedirectToAction("InvalidLogin");
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }
    }
}