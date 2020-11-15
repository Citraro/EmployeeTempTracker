using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
        private int callerID = 117;

        public LoginModel CheckUserLogin(LoginModel loginInfo) {
            WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration ec = WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration.GXPAuthenticationSoap;
            WsAuth.GXPAuthenticationSoapClient svc = new WsAuth.GXPAuthenticationSoapClient(ec);
            WsAuth.LoginResult res = new WsAuth.LoginResult();

            res = svc.Login(loginInfo.DomainName, loginInfo.Username, loginInfo.Password, callerID);
            if (String.IsNullOrEmpty(res.LoginSessionID))
            {
                loginInfo.SessionValid = false;
            }
            else
            {
                loginInfo.SessionId = res.LoginSessionID;
                loginInfo.SessionValid = true;
            }

            return loginInfo;
        }

    }
}