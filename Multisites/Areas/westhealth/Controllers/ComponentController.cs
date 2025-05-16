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
        public ComponentController() {
            _testimonialService = new TestimonialService();
        }
        // GET: westhealth/Index
        public ActionResult Testimonial()
        {
            var testimonialViewModel = _testimonialService.GetTestimonialViewModel();
            return View("~/Areas/westhealth/Views/Components/Testimonial.cshtml",testimonialViewModel); 
        }
    }
}