using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Models.User
{
    public class CurrentUserInfo
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NPIs { get; set; }
    }
}