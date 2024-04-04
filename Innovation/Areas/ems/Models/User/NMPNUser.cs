using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Models.User
{
    public class NMPNUser
    {
        public const string Domain = "NMPN";
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string NPIs { get; set; }
        public bool IsAdmin { get; set; }

    }
}