using System;

namespace EmployeeTempTracker.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
    }
}