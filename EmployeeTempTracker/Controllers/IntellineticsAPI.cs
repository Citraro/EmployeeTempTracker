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

        // Replace with an API call that does this
        public ScreeningModel[] FetchUserScreeningsByDay(int days, string userId = null) {
            ScreeningModel [] screenings = new ScreeningModel[days];
            Random rand = new Random();
            for (int i = 0; i < days; ++i) {
                screenings[i] = new ScreeningModel();
                screenings[i].Date = DateTime.Now.AddDays(-i);
                screenings[i].Temp = (rand.NextDouble()+rand.Next(97, 100)).ToString("F2"); // Random double between 98-99 with 2 decimal points precision
            }
            return screenings;
        }
    }
}