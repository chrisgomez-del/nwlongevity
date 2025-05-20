using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.Innovation.Models.Components;
using NM_MultiSites.Areas.westhealth.Models.Components;
using NM_MultiSites.Areas.westhealth.Services;

namespace NM_MultiSites.Areas.westhealth.Controllers.Components
{
    public class ComponentController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ITestimonialService _testimonialService;
        private readonly IAccordionPanelService _accordionPanelService;
        private readonly IEnhancedCalloutService _enhancedCalloutService;
        public ComponentController() {
            _testimonialService = new TestimonialService();
            _accordionPanelService = new AccordionPanelService();
            _enhancedCalloutService = new EnhancedCalloutService();
            _teamService = new TeamService();

        }
        // GET: westhealth/Index
        public ActionResult Testimonial()
        {
            var testimonialViewModel = _testimonialService.GetTestimonialViewModel();
            return View("~/Areas/westhealth/Views/Components/Testimonial.cshtml",testimonialViewModel); 
        }
        public ActionResult AccordionPanel()
        {
            AccordionPanelViewModel data = _accordionPanelService.GetAccordionPanelData();
            return View("~/Areas/westhealth/Views/Components/AccordionPanel.cshtml",data);
        }

        public ActionResult AccordionItem()
        {
            AccordionItemViewModel data = _accordionPanelService.GetAccordionItemData();
            return View("~/Areas/westhealth/Views/Components/AccordionItem.cshtml",data);
        }
        public ActionResult EnhancedCallout()
        {
            var enhancedCalloutViewModel = _enhancedCalloutService.GetEnhancedCalloutViewModel();
            return View("~/Areas/westhealth/Views/Components/EnhancedCallout.cshtml", enhancedCalloutViewModel);
        }
        public ActionResult TeamContainer()
        {
            var teamContainerViewModel = _teamService.GetTeamContainer();
            return View("~/Areas/westhealth/Views/Components/TeamContainer.cshtml", teamContainerViewModel);
        }
        public ActionResult TeamMember()
        {
            var teamMemberViewModel = _teamService.GetTeamMember();
            return View("~/Areas/westhealth/Views/Components/TeamMember.cshtml", teamMemberViewModel);
        }
    }
}