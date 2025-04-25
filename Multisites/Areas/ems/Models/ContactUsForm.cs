using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Models
{
    public class ContactUsForm
    {
        public ContactUsForm(){}
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Practice { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string skey { get; set; }
        public bool formposted { get; set; }
    }
}