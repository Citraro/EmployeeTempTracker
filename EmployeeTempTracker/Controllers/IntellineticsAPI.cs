using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeTempTracker.Models;
using WsCore;
using WsAuth;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
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
        public ScreeningModel[] FetchUserScreeningsByDay(int days = 7, string userId = null)
        {
            ScreeningModel[] screenings = new ScreeningModel[days];
            Random rand = new Random();
            for (int i = 0; i < days; ++i)
            {
                // 
                screenings[i] = new ScreeningModel();
                screenings[i].EmpId = new int();
                screenings[i].Date = DateTime.Now.AddDays(-i);
                screenings[i].Temp =
                    (rand.NextDouble() + rand.Next(97, 100))
                    .ToString("F2"); // Random double between 98-99 with 2 decimal points precision
            }

            return screenings;
        }

        public WsCore.FMResult InsertScreening(ScreeningModel sm, string domain, string session, int appId)
        {
            WsCore.FMResult result = new FMResult();
            var list = new List<WsCore.FMIndexItem>();

            //list.Add(createIndexItem("acc", "0"));
            list.Add(createIndexItem("EMPLOYEE_ID", sm.EmpId.ToString()));
            list.Add(createIndexItem("LAST_NAME", sm.LastName));
            list.Add(createIndexItem("FIRST_NAME", sm.FirstName));
            list.Add(createIndexItem("DATE", sm.Date.ToString("yyyy-MM-dd")));
            list.Add(createIndexItem("SCREENING_TIME", sm.Date.ToString("hh:mm tt")));
            list.Add(createIndexItem("SYMPTOMS", sm.Symptoms));
            list.Add(createIndexItem("INTL_TRAVEL", sm.IntlTravel));
            list.Add(createIndexItem("SIGNATURE_PRINT_NAME", sm.SigPrintName));
            list.Add(createIndexItem("SIGNATURE_DATE", sm.SigDate.ToString("yyyy-MM-dd")));
            
            // Handle differences in the two domains screening databases
            if (domain == "training1") { // GSI Screening
                list.Add(createIndexItem("TEMPERATURE", sm.Temp));
                list.Add(createIndexItem("CLOSE_CONTACT", sm.CloseContact));
            } else { // Intellinetics Screening
                list.Add(createIndexItem("TEMPERATURE", sm.HighTemp));
                list.Add(createIndexItem("COMPANY", "Intellinetics")); // ToDo: figure out how to differentiate company based off EmpId.
            }

            WsCore.CMCoreServiceSoapClient.EndpointConfiguration ec = WsCore.CMCoreServiceSoapClient.EndpointConfiguration.ICMCoreServiceSoap;
            WsCore.CMCoreServiceSoapClient svc = new WsCore.CMCoreServiceSoapClient(ec);

            result = svc.FMCreateFolder(session, appId, list.ToArray(), 0);
            return result;
        }

        public WsCore.FMResult InsertEmployee(EmployeeModel emp, LoginModel lm,int appId)
        {
            WsCore.FMResult result = new FMResult();
            var list = new List<WsCore.FMIndexItem>();

            var screeningItem = createIndexItem("EMPLOYEE_ID",emp.Id);
            list.Add(screeningItem);
            screeningItem = createIndexItem("LAST_NAME",emp.LastName);
            list.Add(screeningItem);
            screeningItem = createIndexItem("FIRSTNAME",emp.FirstName);
            list.Add(screeningItem);
            screeningItem = createIndexItem("STATUS",emp.Status);
            list.Add(screeningItem);

            WsCore.CMCoreServiceSoapClient.EndpointConfiguration ec = WsCore.CMCoreServiceSoapClient.EndpointConfiguration.ICMCoreServiceSoap;
            WsCore.CMCoreServiceSoapClient svc = new WsCore.CMCoreServiceSoapClient(ec);

            result = svc.FMModifyFolderIndexes(lm.SessionId, appId, Convert.ToInt32(emp.Id), list.ToArray());
            return result;
        }

        private WsCore.FMIndexItem createIndexItem(string name,string value)
        {
            var indexItem = new FMIndexItem();
            indexItem.Name = name;
            indexItem.Value = value;
            return indexItem;
        }
    }
}