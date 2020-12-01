using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.WebUtilities;
using WsCore;
using WsAuth;
using WsSearch;

namespace EmployeeTempTracker.Controllers
{
    public class IntellineticsApi
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string _SERVICE_ASMX = "/ICMCoreService.asmx";
        private const WsCore.CMCoreServiceSoapClient.EndpointConfiguration _ec = WsCore.CMCoreServiceSoapClient.EndpointConfiguration.ICMCoreServiceSoap;
        private  WsCore.CMCoreServiceSoapClient _svc = new WsCore.CMCoreServiceSoapClient(_ec);
        private const WsSearch.CMSearchServiceSoapClient.EndpointConfiguration _sec =
            WsSearch.CMSearchServiceSoapClient.EndpointConfiguration.ICMSearchServiceSoap;
        private  WsSearch.CMSearchServiceSoapClient _search = new CMSearchServiceSoapClient(_sec);

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

            result = _svc.FMCreateFolder(session, appId, list.ToArray(), 0);
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

            result = _svc.FMModifyFolderIndexes(lm.SessionId, appId, Convert.ToInt32(emp.Id), list.ToArray());
            return result;
        }

        public List<EmployeeModel> FetchAllEmployees(string session, int appId) {
            List<EmployeeModel> result = new List<EmployeeModel>();
            var query = _search.Query(session, appId, "", "EMPLOYEE_ID > 0", "", 1, 1, 1000);
            var count = query.XDResultSet.ChildNodes.Count;
            var company = (appId == 117) ? "GSI" : "Intellinetics";
            for(int index = 0; index < count; index++)
            {
                var employee = new EmployeeModel();
                var obj = query.XDResultSet.ChildNodes.Item(index);
                employee.Company = company;
                employee.Id = obj["EMPLOYEE_ID"].InnerText;
                employee.FirstName = obj["FIRST_NAME"].InnerText;
                employee.LastName = obj["LAST_NAME"].InnerText;
                employee.Status = obj["STATUS"].InnerText;
                result.Add(employee);
            }
            return result;
        }

        public EmployeeModel FetchEmployee(int employeeId,string session,int appId)
        {
            var employee = new EmployeeModel();
            var company = appId == 117 ? "GSI" : "Intellinetics";
            var queryString = $"WHERE EMPLOYEE_ID = '{employeeId}'";
            var query = _search.Query(session, appId, "", "", queryString, 1, 1, 1);
            var xmlNode = query.XDResultSet;
            employee.Company = company;
            employee.Id = xmlNode.FirstChild["EMPLOYEE_ID"].InnerText;
            employee.FirstName = xmlNode.FirstChild["FIRST_NAME"].InnerText;
            employee.LastName = xmlNode.FirstChild["LAST_NAME"].InnerText;
            employee.Status = xmlNode.FirstChild["STATUS"].InnerText;
            return employee;
        }

        public List<ScreeningModel> FetchUserScreeningsByDays(int days, string employeeId, string session, int appId)
        {
            List<ScreeningModel> result = new List<ScreeningModel>();
            var today = DateTime.Today;
            var todayMinusDays = DateTime.Today.AddDays(-days);
            var queryString = $"DATE <= '{today}' AND DATE >= '{todayMinusDays}' AND EMPLOYEE_ID = '{employeeId}'";
            try {
                var query = _search.Query(session, appId, "", queryString, "", 1, 1, 1000);
                int count = 0;
                if (query.XDResultSet != null) count = query.XDResultSet.ChildNodes.Count;
                for(int index = 0; index < count; index++)
                {
                    var screening = new ScreeningModel();
                    var obj = query.XDResultSet.ChildNodes.Item(index);
                    screening.EmpId = employeeId;
                    screening.Date = DateTime.Parse(obj["DATE"].InnerText);
                    screening.Time = DateTime.Parse(obj["SCREENING_TIME"].InnerText);
                    screening.LastName = obj["LAST_NAME"].InnerText;
                    screening.FirstName = obj["FIRST_NAME"].InnerText;
                    screening.Temp = obj["TEMPERATURE"].InnerText;
                    screening.Symptoms = obj["SYMPTOMS"].InnerText;
                    screening.CloseContact = obj["CLOSE_CONTACT"].InnerText;
                    screening.IntlTravel = obj["INTL_TRAVEL"].InnerText;
                    screening.SigPrintName = obj["SIGNATURE_PRINT_NAME"].InnerText;
                    screening.SigDate = DateTime.Parse(obj["SIGNATURE_DATE"].InnerText);
                    result.Add(screening);
                }
            } catch (Exception e) {
                log.Debug(e.Message);
            }
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