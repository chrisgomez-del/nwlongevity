using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace NM_MultiSites.Areas.ems.Pipelines
{
    public class RegisterHttpRoutes
    {
        public virtual void Process(PipelineArgs args)
        {
            RegisterRoute(RouteTable.Routes);
        }

        protected virtual void RegisterRoute(RouteCollection routes)
        {

            RouteTable.Routes.MapHttpRoute("PasswordRecovery_Confirm", "sitecore/api/passwordrecovery/{action}/{userName}/{token}", new
            {
                controller = "ConfirmRecovery",
                action = "Index"
            });
            RouteTable.Routes.MapHttpRoute("CIReportApi", "Areas/ems/API/CIReport", new
            {
                controller = "ReportData",
                action = "Index"
            });
        }
    }
}