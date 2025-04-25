using Innovation.Areas.Innovation.Mappers.Components;
using Innovation.Areas.Innovation.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation.Areas.Innovation.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ICardListService _cardListService;
        private readonly IContactInformationService _contactInformationService;
        private readonly IMissionInformationService _missionInfoService;
        private readonly IPortfolioService _portfolioService;
        private readonly IPanelService _panelService;
        private readonly IHeadlineCopyBlockService _headlinecopyblockService;
        private readonly IFocusAreaService _focusAreaService;
        private readonly IStatsComponentService _StatsComponentService;

        public ComponentsController()
        {
            _cardListService = new CardListService();
            _contactInformationService = new ContactInformationService();
            _missionInfoService = new MissionInformationService();
            _portfolioService = new PortfolioService();
            _panelService = new PanelService();
            _headlinecopyblockService = new HeadlineCopyBlockService();
            _focusAreaService = new FocusAreaService();
            _StatsComponentService = new StatsComponentService();
        }
        // GET: Innovation/Components
        public ActionResult EventList()
        {
            EventList CardsContainer = _cardListService.GetEventListData();
            return View(CardsContainer);
        }

        public ActionResult ContactInformation()
        {
            ContactInformation ContactInfoData = _contactInformationService.GetContactInfoData();
            return View(ContactInfoData);
        }

        public ActionResult MissionInformation()
        {
            MissionInformation MissionInfoData = _missionInfoService.GetMissionInfoData();
            return View (MissionInfoData);
        }

        public ActionResult ImageTextCard()
        {
            ImageTextCard data = _portfolioService.GetImageTextCardData();
            return View(data);
        }

        public ActionResult PortfolioSlider()
        {
            CaseStudyList data = _portfolioService.GetCaseStudyData();
            return View(data);
        }
        public ActionResult MultiImageTextCard()
        {
            MultiImageTextCard data = _portfolioService.GetMultiImageTextsectionCardData();
            return View(data);
        }
        public ActionResult CalloutBlock()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }
        public ActionResult CustomHtmlBlock()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }

        public ActionResult HeadlineCopyBlockPanel()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }

        public ActionResult HeadlineCopyBlock()
        {
            HeadlineCopyBlock data = _headlinecopyblockService.GetData();
            return View(data);
        }
        public ActionResult AccordionPanel()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }

        public ActionResult AccordionItem()
        {
            TextBlock data = _cardListService.GetAccordionData();
            return View(data);
        }

        public ActionResult FocusAreaPanel()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }

        public ActionResult FocusAreaCard()
        {
            FocusAreaCard data = _focusAreaService.GetFocusAreaData();
            return View(data);
        }

        public ActionResult NewsFeedPanel()
        {
            BasePanel data = _panelService.GetPanelData();
            return View(data);
        }

        public ActionResult NewsFeedCard()
        {
            NewsFeedCard data = _cardListService.GetNewsFeedData();
            return View(data);
        }
        public ActionResult CaseStudyPagination()
        {
            CaseStudyPagination data = _portfolioService.GetPagenationData();
            return View(data);
        }

        public ActionResult Partner()
        {
            Partner data = _portfolioService.GetPartnerData();
            return View(data);
        }

        public ActionResult StatsComponentPanel()
        {
            StatComponentPanel data = _StatsComponentService.GetStatsComponentPanel();
            return View(data);
        }

        public ActionResult StatsComponentDataPoint()
        {
            StatCardsDataPoint data = _StatsComponentService.GetStatCardsDataPoint();
            return View(data);
        }

        public ActionResult StatsComponentImage()
        {
            StatCardsImage data = _StatsComponentService.GetStatCardsImage();
            return View(data);

        }



    }
}