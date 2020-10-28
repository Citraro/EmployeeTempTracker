using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTempTracker.Models
{
    public class AuthorizationModel
    {
        public string SessionID { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }

    }
}
