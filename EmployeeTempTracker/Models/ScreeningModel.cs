using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTempTracker.Models
{
    public class ScreeningModel
    {
        public string EmpId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        
        public string Temp { get; set; }
        public string Symptoms { get; set; }
        public string CloseContact { get; set; }
        public string IntlTravel { get; set; }
        public string SigPrintName { get; set; }
        [DataType(DataType.Date)]
        public DateTime SigDate { get; set; }
    }
}