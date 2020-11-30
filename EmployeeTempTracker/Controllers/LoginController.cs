using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTempTracker.Controllers {
    public class LoginController : Controller {
        IntellineticsApi api_ = new IntellineticsApi();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private LoginControllerLogic viewProcessor_ = new LoginControllerLogic();

        // GET https://capstone.ohitski.org/Login
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        [HttpPost]
        public async Task<IActionResult> AuthUser(string domain, string uname, string passwd) {
            ViewData["Title"] = "Authenticate";
            try{
                var appID = domain == "training1" ? 117 : 216; //TODO: refactor later
                LoginModel authenticated = api_.CheckUserLogin(new LoginModel(domain, uname, passwd), appID);
            
                if (authenticated.SessionValid) {
                    var claims = new List<Claim>
                    {
                        new Claim("SessionId", authenticated.SessionId),
                        new Claim(ClaimTypes.Name, authenticated.Username),
                        new Claim("Domain", authenticated.DomainName)
                    };
                    Response.Cookies.Append("SessionId", authenticated.SessionId);
                    Response.Cookies.Append("DomainName", authenticated.DomainName);
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            catch(Exception e){
                log.Debug(e.Message);
            }
            return RedirectToAction("InvalidLogin");
        }

        // GET https://capstone.ohitski.org/Login/InvalidLogin
        public IActionResult InvalidLogin() {
            return viewProcessor_.InvalidLogin();
        }
    }
}