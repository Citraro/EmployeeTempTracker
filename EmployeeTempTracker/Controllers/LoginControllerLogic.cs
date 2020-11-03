using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers {
    class LoginControllerLogic : Controller {
        
        // GET https://capstone.ohitski.org/Login
        public IActionResult Index(){
            ViewData["Title"] = "Login";
            return View("Index");
        }

        // POST https://capstone.ohitski.org/Login/AuthUser
        public IActionResult AuthUser(string uname, string passwd, int id = 1) { // TODO: Move functionality to Login()?
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