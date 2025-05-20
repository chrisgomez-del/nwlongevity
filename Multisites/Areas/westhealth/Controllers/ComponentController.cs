using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.Innovation.Models.Components;
using NM_MultiSites.Areas.westhealth.Models.Components;
using NM_MultiSites.Areas.westhealth.Services;

namespace NM_MultiSites.Areas.westhealth.Controllers.Components
{
    public class ComponentController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IAccordionPanelService _accordionPanelService;
        private readonly ICardService _cardService;
        private readonly IEnhancedCalloutService _enhancedCalloutService;
        public ComponentController()
        {
            _testimonialService = new TestimonialService();
            _accordionPanelService = new AccordionPanelService();
            _cardService = new CardService();
            _enhancedCalloutService = new EnhancedCalloutService(_cardService);

        }
        // GET: westhealth/Index
        public ActionResult Testimonial()
        {
            var testimonialViewModel = _testimonialService.GetTestimonialViewModel();
            return View("~/Areas/westhealth/Views/Components/Testimonial.cshtml", testimonialViewModel);
        }
        public ActionResult AccordionPanel()
        {
            AccordionPanelViewModel data = _accordionPanelService.GetAccordionPanelData();
            return View("~/Areas/westhealth/Views/Components/AccordionPanel.cshtml", data);
        }

        public ActionResult AccordionItem()
        {
            AccordionItemViewModel data = _accordionPanelService.GetAccordionItemData();
            return View("~/Areas/westhealth/Views/Components/AccordionItem.cshtml", data);
        }
        public ActionResult EnhancedCallout()
        {
            var enhancedCalloutViewModel = _enhancedCalloutService.GetEnhancedCalloutViewModel();
            return View("~/Areas/westhealth/Views/Components/EnhancedCallout.cshtml", enhancedCalloutViewModel);
        }
    }
}