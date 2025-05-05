using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.ems.Models.User;
using Sitecore.Web.Authentication;
using Sitecore.Security.Accounts;
using Sitecore.Data.Fields;
using NM_MultiSites.Areas.ems.Models;

namespace NM_MultiSites.Areas.ems.Controllers
{
    public class NavigationMenuController : Controller
    {
        public ActionResult NMPN()
        {

            var item = RenderingContext.Current.ContextItem;
            ViewData["IsCurrentUserValid"] = IsCurrentUserValid();
            var homeitem = Sitecore.Context.Database.GetItem("/sitecore/content/NMPN");
            return View(CreateModel(homeitem, item, 0));
        }
        // GET: NavigationMenu
        public ActionResult Index()
        {

            var item = RenderingContext.Current.ContextItem;
            var homeitem = SitecoreAccess.getSiteRoot() ?? RenderingContext.Current.Rendering.Item;
            return View(CreateModel(homeitem, item,0));
        }
        private IEnumerable<NavigationItem> CreateModel(Item root, Item current, int child)
        {
            child++;
            var navigations = root.GetChildren()
                .Where(w => (w.TemplateName == "Pages" || w.TemplateName == "Page" || w.TemplateName == "Events")
                && !((Sitecore.Data.Fields.CheckboxField)w.Fields["ExcludeFromNavigation"]).Checked)
                .ToList();
            if (child== 1 && !((Sitecore.Data.Fields.CheckboxField)root.Fields["ExcludeFromNavigation"]).Checked)
            {
                navigations.Insert(0, root);

            }
            IEnumerable<NavigationItem> navitem = navigations.Select(s => new NavigationItem()
            {  
                Title = s.Fields["NavigationTitle"].GetValue(true),
                URL = LinkManager.GetItemUrl(s),
                Active = (s.ID == current.ID),
                itemid = s.ID.ToString(),
                IsSecurePage = Page.isAuthorized(s),
                Children = s.HasChildren && child<2 && s!=root? CreateModel(s,s,child):null
            }
            );
            return navitem;
        }
        private bool IsCurrentUserValid()
        {
            bool isValid = false;
            var logincookie = System.Web.HttpContext.Current.Request.Cookies["sitecore_userticket"];
            if (logincookie != null && !String.IsNullOrEmpty(logincookie.Value))
            {
                Ticket currentticket = Sitecore.Web.Authentication.TicketManager.GetTicket(logincookie.Value);
                if (currentticket == null)
                {
                    return isValid;
                }
                isValid = LoginHelper.IsValidUser(currentticket.UserName);
            }
            else
            {
                isValid = false;
            }
            return isValid;

        }
    }
}