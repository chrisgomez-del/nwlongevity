using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.westhealth.Controllers.Components
{
    public class NavigationController : Controller
    {
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
    }
}