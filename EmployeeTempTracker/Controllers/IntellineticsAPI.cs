using System;
using EmployeeTempTracker.Models;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
        private int callerID = 117;
        
        public LoginModel CheckUserLogin(LoginModel loginInfo)
        {
            WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration ec = WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration.GXPAuthenticationSoap;
            WsAuth.GXPAuthenticationSoapClient svc = new WsAuth.GXPAuthenticationSoapClient(ec);
            WsAuth.LoginResult res = new WsAuth.LoginResult();

            res = svc.Login(loginInfo.DomainName, loginInfo.Username, loginInfo.Password, callerID);
            if (String.IsNullOrEmpty(res.LoginSessionID))
            {
                loginInfo.SessionValid = true;
                loginInfo.SessionId = res.LoginSessionID;
            }
            else
            {
                loginInfo.SessionValid = false;
            }

            return loginInfo;
        }
    }
}