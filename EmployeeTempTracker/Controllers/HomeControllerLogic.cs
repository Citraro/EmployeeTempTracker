using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;
using Newtonsoft.Json;

namespace EmployeeTempTracker.Controllers {
    
    class HomeControllerLogic : Controller {
        private static IntellineticsApi api_ = new IntellineticsApi();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Placeholder
        public IActionResult Index() {
            ViewData["Title"] = "Home";
            return View("Dashboard");
        }

        // GET https://capstone.ohitski.org/Home/Dashboard
        public IActionResult Dashboard(string domain = null,string sessionId = null) {
            ViewData["DomainName"] = domain;
            var appId = (domain == "training1") ? 117 : 216;
            List<EmployeeModel> contents = api_.FetchAllEmployees(sessionId, appId);
            ViewData["DashboardContents"] = JsonConvert.SerializeObject(contents.ToArray());
            return View("Dashboard");
        }

        public IActionResult History(string sessionId, string domain) {
            try {
                int appId = (domain == "training1") ? 117 : 216; // Employee table
                List<EmployeeModel> employees = api_.FetchAllEmployees(sessionId, appId);
                List<ScreeningModel> graphData = new List<ScreeningModel>();

                appId = (domain == "training1") ? 116 : 217; // Switch to screening table
                double maxTemp = -1.0, minTemp = 200.0, sum = 0.0;
                int recordCount = 0;
                foreach (EmployeeModel employee in employees) {
                    ScreeningModel record = api_.FetchUserScreeningsByDays(2, employee.Id, sessionId, appId)[0];
                    double current = Convert.ToDouble(record.Temp);
                    maxTemp = (maxTemp < current) ? current : maxTemp;
                    minTemp = (minTemp > current) ? current : minTemp;
                    sum += current;
                    graphData.Add(record);
                    ++recordCount;
                }
                double average = sum / recordCount;
                double rangeHalf = (maxTemp - minTemp) / 2;

                // Pass data to the view
                ViewData["Name"] = "All Employees";
                ViewData["Timeframe"] = "Today";
                ViewData["Average"] = average.ToString("F2");
                ViewData["MinTemp"] = minTemp.ToString("F2");
                ViewData["MaxTemp"] = maxTemp.ToString("F2");
                ViewData["ExpectedRange"] = $"{(average - rangeHalf).ToString("F2")} - {(average + rangeHalf).ToString("F2")}";
                ViewData["Screenings"] = JsonConvert.SerializeObject(graphData);
            } catch (Exception e) {
                log.Debug(e.Message);
            }
            return View("Analytics");
            
        }
        public IActionResult Analytics(string id, string fname, string lname, string sessionId, int appId, int numDays) {
            try{
                List<ScreeningModel> graphData = api_.FetchUserScreeningsByDays(numDays, id, sessionId, appId);

                // Start computing statistics
                double maxTemp = -1.0, minTemp = 200.0, sum = 0.0;
                int recordCount = 0;
                foreach (ScreeningModel obj in graphData) {
                    double current = Convert.ToDouble(obj.Temp);
                    maxTemp = (maxTemp < current) ? current : maxTemp;
                    minTemp = (minTemp > current) ? current : minTemp;
                    sum += current;
                    ++recordCount;
                }
                double average = sum / recordCount;
                double rangeHalf = (maxTemp - minTemp) / 2;

                // Pass data to the view
                ViewData["Name"] = $"{fname} {lname}";
                ViewData["Timeframe"] = "7 days";
                ViewData["Average"] = average.ToString("F2");
                ViewData["MinTemp"] = minTemp.ToString("F2");
                ViewData["MaxTemp"] = maxTemp.ToString("F2");
                ViewData["ExpectedRange"] = $"{(average - rangeHalf).ToString("F2")} - {(average + rangeHalf).ToString("F2")}";
                ViewData["Screenings"] = JsonConvert.SerializeObject(graphData);
            }
            catch(Exception e){
                log.Debug(e.Message);
            }

            return View();
        }

        // GET https://capstone.ohitski.org/Home/Error
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
    
}