using Microsoft.AspNetCore.Mvc;
using EmployeeTempTracker.Models;
using System;

namespace EmployeeTempTracker.Controllers {
    class EmployeeControllerLogic : Controller {
        LoginController login = new LoginController();

        // GET https://capstone.ohitski.org/Employee
        public IActionResult Index() {
            bool authenticated = true;
            if (!authenticated) return RedirectToAction("Index", "Login");
            
            return View("Index");
        }

        // GET https://capstone.ohitski.org/Employee/CreateEmployee
        public IActionResult CreateEmployee() {
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");
            ViewData["Title"] = "Create Employee";

            return View("CreateEmployee","Employee");            
        }

        public IActionResult CreateEmployeeModel(string id, string company, string lastName, string firstName, string status){
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            EmployeeModel employee = new EmployeeModel();
            employee.Id = id;
            employee.Company = company;
            employee.LastName = lastName;
            employee.FirstName = firstName;
            employee.Status = status;
            ViewData["Employee"] = employee;

            //TODO: SOME LOGIC TO ADD EMPLOYEE TO DATABASE

            return RedirectToAction("ReadEmployee", employee);
        }

        // GET https://capstone.ohitski.org/Employee/ReadEmployee
        public IActionResult ReadEmployee(EmployeeModel employee) {
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            ViewData["Title"] = "Read Employee";
            ViewData["Employee"] = employee; 
            return View("ReadEmployee", "Employee");
        }

        // GET https://capstone.ohitski.org/Employee/UpdateEmployee
        public IActionResult UpdateEmployee(EmployeeModel employee) {
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            ViewData["Title"] = "Update Employee";
            ViewData["Employee"] = employee; 
            return View("UpdateEmployee", "Employee");
        }

        // GET https://capstone.ohitski.org/Employee/DeleteEmployee
        public IActionResult DeleteEmployee(EmployeeModel employee) { 
            bool authenticated = true; // TODO: Replace with session check
            if (!authenticated) return RedirectToAction("Index", "Login");

            ViewData["Title"] = "Delete Employee";
            ViewData["Employee"] = employee; 
            return View("DeleteEmployee", "Employee"); 
        }

    }
}