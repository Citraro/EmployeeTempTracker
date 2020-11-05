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
        public IActionResult AuthUser(string domain, string uname, string passwd) { // TODO: Move functionality to Login()?
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

        // GET https://capstone.ohitski.org/Login/Login
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

        // POST https://capstone.ohitski.org/Login/Login
        public IActionResult Login(LoginModel lm) {
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

    }
}