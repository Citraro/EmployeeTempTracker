using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeTempTracker.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using WsAuth;

namespace EmployeeTempTracker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string DomainName, bool? SessionValid, string Username)
        {
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel lm)
        {
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
                    return View(lm);
                }
                else
                {
                    am.SessionID = res.LoginSessionID;
                    am.UserName = lm.Username;
                    am.Domain = lm.DomainName;
                    return View(lm);
                }
            }
            else
            {
                return View(lm);
            }
        }
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }*/
    }
}
