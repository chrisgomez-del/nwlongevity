using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.westhealth.Services;

namespace NM_MultiSites.Areas.westhealth.Controllers.Components
{
    public class NavigationController : Controller
    {
        private readonly INavigationService _navigationService;
        public NavigationController()
        {
            _navigationService = new NavigationService();
        }
        public ActionResult Header()
        {
            var headerViewModel = _navigationService.GetHeader();
            return View(headerViewModel);
        }
        public ActionResult Footer()
        {
            var footerViewModel = _navigationService.GetFooter();
            return View(footerViewModel);
        }
        public ActionResult InternalNavigation()
        {
            var navigationViewModel = _navigationService.GetInternalNavigation();
            return View(navigationViewModel);
        }
    }
}