using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTempTracker.Models
{
    public class ScreeningModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        
        public string Temp { get; set; }

        public string HighTemp{ get; set; }
        public string Symptoms { get; set; }
        public string CloseContact { get; set; }
        public string IntlTravel { get; set; }

        public string Sig { get; set;}
        public string SigPrintName { get; set; }
        [DataType(DataType.Date)]
        public DateTime SigDate { get; set; }

        public ScreeningModel() {
            EmpId = new int();
            Date = DateTime.Now;
            Time = DateTime.Now;
            Temp = "98.6";
            Symptoms = "No";
            CloseContact = "No";
            IntlTravel = "No";
            HighTemp = "No";
            SigPrintName = null;
            SigDate = DateTime.Now;
        }

    }

}