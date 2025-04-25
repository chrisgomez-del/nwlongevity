using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Innovation.Areas.Innovation.Mappers.Global;
using Innovation.Areas.Innovation.Models.Global;

namespace Innovation.Areas.Innovation.Controllers
{
    public class GlobalComponentsController : Controller
    {
        private readonly IHeaderService _headerService;
        private readonly IFooterService _footerService;
        public GlobalComponentsController()
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