using NM_MultiSites.Areas.Innovation.Helpers;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace NM_MultiSites.App_Start
{
    public class RegisterWebApiRoutes
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            var config = GlobalConfiguration.Configuration;

            routes.MapHttpRoute("CaptchaAPIRoute",
                                "api/Captcha/ValidResponse",
                                new {
                                    controller = "Captcha",
                                    action = "ValidCaptchaResponse",
                                });
            routes.MapHttpRoute("CaptchaAPIRouteTest",
                                "api/Captcha/APITest",
                                new
                                {
                                    controller = "Captcha",
                                    action = "CaptchaResponseTest"
                                });

        }
    }
}