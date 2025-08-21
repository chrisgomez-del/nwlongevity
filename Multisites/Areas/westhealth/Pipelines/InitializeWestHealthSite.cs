using Sitecore.Pipelines;
using System.Web.Routing;
using NM_MultiSites.App_Start;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.westhealth.Pipelines
{
    public class InitializeWestHealthSite
    {
        public void Process(PipelineArgs args)
        {
            // Clear the Globalization Cache
            Sitecore.Globalization.Translate.ResetCache(true);
        }

    }
}
