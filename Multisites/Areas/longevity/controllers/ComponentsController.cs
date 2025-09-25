using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Mappers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.Innovation.Controllers
{
    public class LongevityComponentsController : Controller
    {
        private readonly IFullAssessmentService _fullAssessmentService;
        private readonly ITimelineService _timeLineService;
        private readonly IFlippingCardsService _flippingCardsService;
        private readonly ITeamMembersService _teamMembersService;
        private readonly IRightVideoService _rightVideoService;
        private readonly ITransitionCopyService _transitionCopyService;
        private readonly IVideoCarouselService _videoCarouselService;
        private readonly IWhoWeAreService _whoWeAreService;


        public LongevityComponentsController()
        {
            _fullAssessmentService = new FullAssessmentService();
            _timeLineService = new TimelineService();
            _flippingCardsService = new FlippingCardsService();
            _teamMembersService = new TeamMembersService();
            _rightVideoService = new RightVideoService();
            _transitionCopyService = new TransitionCopyService();
            _videoCarouselService = new VideoCarouselService();
            _whoWeAreService = new WhoWeAreService();
        }

        public ActionResult FullAssessment()
        {
            FullAssessment fullAssessment = _fullAssessmentService.GetAssessmentData();
            return View(fullAssessment);
        }

        public ActionResult Timeline()
        {
            Timeline timeline = _timeLineService.GetTimelineData();
            return View(timeline);
        }

        public ActionResult FlippingCards()
        {
            FlippingCards flippingCards = _flippingCardsService.GetFlippingCardsData();
            return View(flippingCards);
        }

        public ActionResult TeamMembers()
        {
            TeamMembers teamMembers = _teamMembersService.GetTeamMembersData();
            return View(teamMembers);
        }

        public ActionResult RightVideo()
        {
            RightVideo rightVideo = _rightVideoService.GetRightVideoData();
            return View(rightVideo);
        }

        public ActionResult TransitionCopy()
        {
            TransitionCopy transitionCopy = _transitionCopyService.GetTransitionCopyData();
            return View(transitionCopy);
        }

        public ActionResult VideoCarousel()
        {
            VideoCarousel videos = _videoCarouselService.GetVideoData();
            return View(videos);
        }

        public ActionResult WhoWeAre()
        {
            WhoWeAre whoWeAre = _whoWeAreService.GetWhoWeAreData();
            return View(whoWeAre);
        }
    }
}