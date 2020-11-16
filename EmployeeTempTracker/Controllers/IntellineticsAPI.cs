using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeTempTracker.Models;
using WsCore;
using WsAuth;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
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