using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.westhealth.Models.Components;
using NM_MultiSites.Areas.westhealth.Services;

namespace NM_MultiSites.Areas.westhealth.Controllers.Components
{
    public class ComponentController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IEnhancedCalloutService _enhancedCalloutService;
        public ComponentController() {
            _testimonialService = new TestimonialService();
            _enhancedCalloutService = new EnhancedCalloutService(); 

        }
        // GET: westhealth/Index
        public ActionResult Testimonial()
        {
            var testimonialViewModel = _testimonialService.GetTestimonialViewModel();
            return View("~/Areas/westhealth/Views/Components/Testimonial.cshtml",testimonialViewModel); 
        }

        public ActionResult EnhancedCallout()
        {
            var enhancedCalloutViewModel = _enhancedCalloutService.GetEnhancedCalloutViewModel();
            return View("~/Areas/westhealth/Views/Components/EnhancedCallout.cshtml", enhancedCalloutViewModel);
        }
    }
}