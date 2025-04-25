using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using EMS.ems.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using EMS.Areas.ems.Helper;
using EMS.Areas.ems.Models;

namespace EMS.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Events()
        {
            return View(GetEvents());
        }
        // GET: NavigationMenu
        public ActionResult EventDetail()
        {
            return View(GetEvent());
        }

        private EventDetail GetEvent()
        {
            Item i = PageContext.Current.Item;
            return new EventDetail(){
                        Item = i,
                        Title = new HtmlString(FieldRenderer.Render(i, "Title")),
                        ShortText = new HtmlString(FieldRenderer.Render(i, "Short Text")),
                        MainContent = new HtmlString(FieldRenderer.Render(i, "MainContent")),
                        AdditionalContent = new HtmlString(FieldRenderer.Render(i, "AdditionalContent")),
                        StartDate = new HtmlString(FieldRenderer.Render(i, "Start Date", "format=MMMM d, yyyy - h:mm tt")),
                        EndDate = new HtmlString(FieldRenderer.Render(i, "End Date", "format=MMMM d, yyyy - h:mm tt")),
                        Image = SitecoreAccess.GetMediaUrl(i),
                        ImageFullPath = i.Paths.FullPath,
                        CTATitle = String.IsNullOrEmpty(i.Fields["CTA Title"].GetValue(true)) ? null : new HtmlString(FieldRenderer.Render(i, "CTA Title")),
                        CTALink = String.IsNullOrEmpty(i.Fields["CTA Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(i.Fields["CTA Link"]),
                    };
        }

        private EventCollection GetEvents()
        {


            var EventCollection = new EventCollection();
            try
            {
                var item = PageContext.Current.Item;
                List<Item> rows = SitecoreAccess.GetMultiListItems(item, "EventList");
                foreach(Item i in rows)
                {
                    EventCollection.Events.Add(new EventDetail()
                    {
                        Item = i,
                        Title = new HtmlString(FieldRenderer.Render(i, "Title")),
                        ShortText = new HtmlString(FieldRenderer.Render(i, "Short Text")),
                        MainContent = new HtmlString(FieldRenderer.Render(i, "MainContent")),
                        StartDate = new HtmlString(FieldRenderer.Render(i, "Start Date", "format=MMMM d, yyyy - h:mm tt")),
                        EndDate = new HtmlString(FieldRenderer.Render(i, "End Date", "format=MMMM d, yyyy - h:mm tt")),
                        Image = SitecoreAccess.GetMediaUrl(i),
                        ImageFullPath = i.Paths.FullPath,
                        CTATitle = String.IsNullOrEmpty(i.Fields["CTA Title"].GetValue(true)) ? null : new HtmlString(FieldRenderer.Render(i, "CTA Title")),
                        CTALink = String.IsNullOrEmpty(i.Fields["CTA Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(i.Fields["CTA Link"]),
                    });
                }
                EventCollection.Title = new HtmlString(FieldRenderer.Render(item, "Title"));
                return EventCollection;
            }
            catch (Exception ex)
            {
                Log.Error("Exception :" + ex, this);
            }

            return null;

        }
    }
}