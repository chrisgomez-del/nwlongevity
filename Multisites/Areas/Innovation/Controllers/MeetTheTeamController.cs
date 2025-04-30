using NM_MultiSites.Areas.Innovation.Mappers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.Innovation.Controllers
{
    public class MeetTheTeamController : Controller
    {
        private readonly IMeetTheTeamService _meetTheTeamService;
        private readonly IPanelService _panelService;
        public MeetTheTeamController()
        {
            _meetTheTeamService = new MeetTheTeamService();
            _panelService = new PanelService();
        }
        // GET: Innovation/MeetTheTeam
        public ActionResult Bio()
        {
            var data = _meetTheTeamService.GetBioData();
            return View(data);
        }
        public ActionResult BioPanel()
        {
            var data = _panelService.GetPanelData();
            return View(data);
        }
    }
}