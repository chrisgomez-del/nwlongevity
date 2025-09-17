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
        private readonly IAssessmentService _assessmentService;
        private readonly IExperienceService _experienceService;
        private readonly IFlippingCardsService _flippingCardsService;
        private readonly IOurTeamService _ourTeamService;
        private readonly IRightVideoService _rightVideoService;
        private readonly ITransitionCopyService _transitionCopyService;
        private readonly IVideoCarouselService _videoCarouselService;


        public LongevityComponentsController()
        {
            _assessmentService = new AssessmentService();
            _experienceService = new ExperienceService();
            _flippingCardsService = new FlippingCardsService();
            _ourTeamService = new OurTeamService();
            _rightVideoService = new RightVideoService();
            _transitionCopyService = new TransitionCopyService();
            _videoCarouselService = new VideoCarouselService();
        }

        public ActionResult Assessment()
        {
            Assessment assessment = _assessmentService.GetAssessmentData();
            return View(assessment);
        }

        public ActionResult Experience()
        {
            Experience experience = _experienceService.GetExperienceData();
            return View(experience);
        }

        public ActionResult FlippingCards()
        {
            FlippingCards flippingCards = _flippingCardsService.GetFlippingCardsData();
            return View(flippingCards);
        }

        public ActionResult OurTeam()
        {
            OurTeam ourTeam = _ourTeamService.GetOurTeamData();
            return View(ourTeam);
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

    }
}