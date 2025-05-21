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
        private readonly ITeamService _teamService;
        private readonly ITestimonialService _testimonialService;
        private readonly IAccordionPanelService _accordionPanelService;
        private readonly ICardService _cardService;
        private readonly IEnhancedCalloutService _enhancedCalloutService;
        private readonly ITwoColumnWithImageService _twoColumnWithImageService;
        private readonly ISliderService _sliderService;
        private readonly ITwoColumnImageStackService _twoColumnImageStackService;

        public ComponentController()
        {
            _testimonialService = new TestimonialService();
            _accordionPanelService = new AccordionPanelService();
            _teamService = new TeamService();
            _cardService = new CardService();
            _enhancedCalloutService = new EnhancedCalloutService();
            _cardService = new CardService();
            _twoColumnWithImageService = new TwoColumnWithImageService();
            _sliderService = new SliderService();
            _twoColumnImageStackService = new TwoColumnImageStackService();

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
        public ActionResult Card()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Card.cshtml", cardViewModel);
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
        public ActionResult TwoColumnWithImage()
        {
            var twoColumnWithImageViewModel = _twoColumnWithImageService.GetTwoColumnWithImageViewModel(); 
            return View("~/Areas/westhealth/Views/Components/TwoColumnWithImage.cshtml", twoColumnWithImageViewModel);
        }
        public ActionResult Slider()
        {
            var sliderViewModel = _sliderService.GetSliderViewModel();
            return View("~/Areas/westhealth/Views/Components/Slider.cshtml", sliderViewModel);
        }
        public ActionResult Slide()
        {
            var slideViewModel = _sliderService.GetSlideViewModel();
            return View("~/Areas/westhealth/Views/Components/Slide.cshtml", slideViewModel);
        }
        public ActionResult TwoColumnImageStack()
        {
            var twoColumnImageStackViewModel = _twoColumnImageStackService.GetTwoImageStackViewModel();
            return View("~/Areas/westhealth/Views/Components/TwoColumnImageStack.cshtml", twoColumnImageStackViewModel);
        }
    }
}