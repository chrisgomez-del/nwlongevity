using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using Sitecore.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Helper
{
    public static class Page
    {
        public static bool isAuthorized(Item item)
        {
            string field = "IsAuthenticatedPage";
            if (item != null && item.Fields[field] != null && !String.IsNullOrWhiteSpace(item.Fields[field].Value))
            {
                return item.Fields[field].Value.ToString() == "1" ? true : false;

            }
            return false;
        }


        public static string GetCurrentUserNPI()
        {
            string NPIs = String.Empty;
            var logincookie = System.Web.HttpContext.Current.Request.Cookies["sitecore_userticket"];
            if (logincookie != null && !String.IsNullOrEmpty(logincookie.Value))
            {
                //Need to handle
                Ticket currentticket = Sitecore.Web.Authentication.TicketManager.GetTicket(logincookie.Value);
                if (currentticket == null)
                {
                    return NPIs;
                }
                User currentUser = LoginHelper.GetUser(currentticket.UserName);
                NPIs = currentUser.Profile.GetCustomProperty("IsNMPNAdmin") == "1" ? "ADM" : currentUser.Profile.GetCustomProperty("NPI Ids");
                return NPIs;
            }
            else
            {
                return NPIs;
            }
        }
    }
}