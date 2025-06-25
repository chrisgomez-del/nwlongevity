using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
        private readonly ITwoColumnStaggeredListService _twoColumnStaggeredListService;
        private readonly IResearchListService _researchListService;
        private readonly ISplitContentHeroService _splitContentHeroService;
        private readonly ISplitContentSubtitleService _splitContentSubtitleService;
        private readonly ICardListService _cardListService;
        private readonly IThreeCardCalloutService _threeCardCalloutService;
        private readonly INavigableTabsService _navigableTabsService;
        private readonly IRingDiagramService _ringDiagramService; 

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
            _twoColumnStaggeredListService = new TwoColumnStaggeredListService();
            _researchListService = new ResearchListService();
            _splitContentHeroService = new SplitContentHeroService(); 
            _splitContentSubtitleService = new SplitContentSubtitleService();
            _cardListService = new CardListService();
            _threeCardCalloutService = new ThreeCardCalloutService();
            _navigableTabsService = new NavigableTabsService();
            _ringDiagramService = new RingDiagramService();

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
        public ActionResult TwoColumnStaggeredList()
        {
            var twoColumnStaggeredListViewModel = _twoColumnStaggeredListService.GetTwoColumnStaggeredListViewModel();
            return View("~/Areas/westhealth/Views/Components/TwoColumnStaggeredList.cshtml", twoColumnStaggeredListViewModel);
        }
        public ActionResult ResearchList()
        {
            var researchListViewModel = _researchListService.GetResearchListViewModel();
            return View("~/Areas/westhealth/Views/Components/ResearchList.cshtml", researchListViewModel);
        }
        public ActionResult SplitContentHero()
        {
            var splitContentHeroViewModel = _splitContentHeroService.GetSplitContentHeroViewModel();
            return View("~/Areas/westhealth/Views/Components/SplitContentHero.cshtml", splitContentHeroViewModel);
        }
        public ActionResult SplitContentSubtitle()
        {
            var splitContentSubtitleViewModel = _splitContentSubtitleService.GetSplitContentSubtitleViewModel();
            return View("~/Areas/westhealth/Views/Components/SplitContentSubtitle.cshtml", splitContentSubtitleViewModel);
        }
        public ActionResult HeroCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/HeroCard.cshtml", cardViewModel);
        }
        public ActionResult SubtitleCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/SubtitleCard.cshtml", cardViewModel);
        }
        public ActionResult TwoColumnCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/TwoColumnCard.cshtml", cardViewModel);
        }
        public ActionResult TwoColumnCardStaggered()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/TwoColumnCardStaggered.cshtml", cardViewModel);
        }
        public ActionResult EnhancedCalloutCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/EnhancedCalloutCard.cshtml", cardViewModel);
        }
        public ActionResult CardList()
        {
            var cardListViewModel = _cardListService.GetCardListViewModel();
            return View("~/Areas/westhealth/Views/Components/CardList.cshtml", cardListViewModel);
        }
        public ActionResult NewsCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/NewsCard.cshtml", cardViewModel);
        }
        public ActionResult ResourceCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/ResourceCard.cshtml", cardViewModel);
        }
        public ActionResult ThreeCardCallout()
        {
            var threeCardCalloutViewModel = _threeCardCalloutService.GetThreeCardCalloutViewModel();
            return View("~/Areas/westhealth/Views/Components/ThreeCardCallout.cshtml", threeCardCalloutViewModel);
        }
        public ActionResult StatCard()
        {
            var cardViewModel = _cardService.GetCardViewModel();
            return View("~/Areas/westhealth/Views/Components/Cards/StatCard.cshtml", cardViewModel);
        }
        public ActionResult NavigableTabs()
        {
            var navigableTabsViewModel = _navigableTabsService.GetNavigableTabsViewModel(); 
            return View("~/Areas/westhealth/Views/Components/NavigableTabs.cshtml", navigableTabsViewModel);
        }
        public ActionResult RingDiagram()
        {
            var ringDiagramViewModel = _ringDiagramService.GetRingDiagramViewModel(); 
            return View("~/Areas/westhealth/Views/Components/RingDiagram.cshtml", ringDiagramViewModel);
        }
        public ActionResult TextSlide()
        {
            var slideViewModel = _sliderService.GetSlideViewModel();
            return View("~/Areas/westhealth/Views/Components/TextSlide.cshtml", slideViewModel);
        }
    }
}