using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.Longevity.Mappers.Global;
using NM_MultiSites.Areas.Longevity.Models.Global;

namespace NM_MultiSites.Areas.Longevity.Controllers
{
    public class LongevityGlobalComponentsController : Controller
    {
        private readonly IHeaderService _headerService;
        private readonly IFooterService _footerService;
        public LongevityGlobalComponentsController()
        {
            _headerService = new HeaderService();
            _footerService = new FooterService();
        }

        // GET: Innovation/GlobalComponents
        public ActionResult Header()
        {
            Header HeaderData = _headerService.GetHeader();
            return View(HeaderData);
        }
        public ActionResult Footer()
        {
            Footer FooterData = _footerService.GetFooter();
            return View(FooterData);
        }
    }
}