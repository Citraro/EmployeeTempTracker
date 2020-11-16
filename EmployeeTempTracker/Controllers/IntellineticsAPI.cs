using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EmployeeTempTracker.Models;
using WsCore;
using WsAuth;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
        private int callerID = 117;

        public LoginModel CheckUserLogin(LoginModel loginInfo) {
            WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration ec = WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration.GXPAuthenticationSoap;
        private WsCore.ICMCoreServiceSoap _service;
        private const string _SERVICE_ASMX = "/ICMCoreService.asmx";

        public LoginModel CheckUserLogin(LoginModel loginInfo, int appId)
        {
            WsAuth.GXPAuthenticationSoapClient.EndpointConfiguration ec = WsAuth.GXPAuthenticationSoapClient
                .EndpointConfiguration.GXPAuthenticationSoap;
            WsAuth.GXPAuthenticationSoapClient svc = new WsAuth.GXPAuthenticationSoapClient(ec);
            WsAuth.LoginResult res = new WsAuth.LoginResult();

            res = svc.Login(loginInfo.DomainName, loginInfo.Username, loginInfo.Password, appId);
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
        // Replace with an API call that does this
        public ScreeningModel[] FetchUserScreeningsByDay(int days = 7, string userId = null) {
            ScreeningModel [] screenings = new ScreeningModel[days];
            Random rand = new Random();
            for (int i = 0; i < days; ++i) { // 
                screenings[i] = new ScreeningModel();
                screenings[i].EmpId = "Example";
                screenings[i].Date = DateTime.Now.AddDays(-i);
                screenings[i].Temp = (rand.NextDouble()+rand.Next(97, 100)).ToString("F2"); // Random double between 98-99 with 2 decimal points precision
            }
            return screenings;
        public WsCore.FMResult InsertScreening(ScreeningModel sm, LoginModel lm,int appId)
        {
            WsCore.FMResult result = new FMResult();
            var list = new List<WsCore.FMIndexItem>();
            WsCore.FMIndexItem screeningItem = new WsCore.FMIndexItem();
            screeningItem.Id = sm.EmpId;
            //TODO: add full screening record
            list.Append(screeningItem);
            result = _service.FMModifyFolderIndexes(lm.SessionId, appId, sm.EmpId, list.ToArray());
            return result;
        }
    }
}