using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTempTracker.Models
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Domain Name")]
        public string DomainName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        // Not sure we want to allow this option
        [DisplayName("Keep Me Logged In")]
        public bool RememberMe { get; set; }
        public bool SessionValid { get; set; }
        public string SessionId { get; set; }

        public LoginModel() {
            DomainName =    null;
            Username =      null;
            Password =      null;
            RememberMe =    false;
            SessionValid =  false;
            SessionId =     null;   
        }

        public LoginModel(string domain, string user, string pass) {
            DomainName =    domain;
            Username =      user;
            Password =      pass;
            RememberMe =    false;
            SessionValid =  false;
            SessionId =     null;
        }
    }

}