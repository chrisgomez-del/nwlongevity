using System.Web.Routing;
using Innovation.App_Start;
using Sitecore.Pipelines;
using System.Web.Mvc;


namespace Innovation.Areas.Innovation.Pipelines
{
    public class InitializeInnovationSite
    {
        public void Process(PipelineArgs args)
        {
            // Register Routes
            RegisterWebApiRoutes.RegisterRoutes(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;

            // Clear the Globalization Cache
            Sitecore.Globalization.Translate.ResetCache(true);
        }
    }
}