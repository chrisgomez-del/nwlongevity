using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NM_MultiSites.Areas.westhealth.Services;

namespace NM_MultiSites.Areas.westhealth.Controllers
{
    public class PageController : Controller
    {
        private readonly IArticleService _articleService;

        public PageController()
        {
            _articleService = new ArticleService();

        }

        // GET: westhealth/Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Article()
        {
            var articleViewModel = _articleService.GetArticleViewModel();
            return View("~/Areas/westhealth/Views/Pages/Article.cshtml", articleViewModel);
        }
    }
}