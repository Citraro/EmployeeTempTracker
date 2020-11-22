using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace EmployeeTempTracker.Controllers {
    [Authorize]
    public class EmployeeController : Controller {
 
        private EmployeeControllerLogic viewProcessor_ = new EmployeeControllerLogic();

        // GET https://capstone.ohitski.org/Employee
        public IActionResult Index() {
            return viewProcessor_.Index();
        }

        // GET https://capstone.ohitski.org/Employee/CreateEmployee
        public IActionResult CreateEmployee() {
                return viewProcessor_.CreateEmployee();
        }

        // GET https://capstone.ohitski.org/Employee/CreateEmployeeModel
        public IActionResult CreateEmployeeModel(string id, string company, string lastName, string firstName, string status) {

            return viewProcessor_.CreateEmployeeModel(id, company, lastName, firstName, status);
        }

        // GET https://capstone.ohitski.org/Employee/ReadEmployee
        public IActionResult ReadEmployee(EmployeeModel employee) {
            return viewProcessor_.ReadEmployee(employee);
        }

        // GET https://capstone.ohitski.org/Employee/UpdateEmployee
        public IActionResult UpdateEmployee(EmployeeModel employee) { 
            return viewProcessor_.UpdateEmployee(employee);
        }

        // POST https://capstone.ohitski.org/Employee/DeleteEmployee
        public IActionResult DeleteEmployee(EmployeeModel employee) {
            return viewProcessor_.DeleteEmployee(employee);
        }

    }
}