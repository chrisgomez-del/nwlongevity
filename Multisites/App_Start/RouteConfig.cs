using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace NM_MultiSites.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "JsonResponse",
                url: "JsonResponse",
                new { controller = "JsonResponse", action = "Index" },
                new[] { "Innovation.Areas.Innovation.Controllers" });

            //routes.MapHttpRoute("PasswordRecovery_Confirm", "sitecore/api/passwordrecovery/{action}/{userName}/{token}", new
            //{
            //    controller = "ConfirmRecovery",
            //    action = "Index"
            //});
        }
    }
}
