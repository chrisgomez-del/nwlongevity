using Innovation.Areas.Innovation.Mappers.Components;
using Innovation.Areas.Innovation.Models.Components.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation.Areas.Innovation.Controllers
{
    public class TabController : Controller
    {
        private readonly IFocusAreaService _focusAreaService;

        public TabController()
        {
            _focusAreaService = new FocusAreaService();
        }

        public ActionResult TabLabel()
        {
            TabLabel data = _focusAreaService.GetTabLabel();
            return View(data);
        }

        public ActionResult TabPanel()
        {
            TabPanel data = _focusAreaService.GetTabPanel();
            return View(data);
        }

        public ActionResult ContentTab()
        {
            ContentTab data = _focusAreaService.GetContentTab();
            return View(data);
        }
    }
}