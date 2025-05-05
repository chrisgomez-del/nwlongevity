using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class FormMessage
    {
        public bool Success { get; set; }

        public HtmlString Message { get; set; }
    }
}