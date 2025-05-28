using System.Web.Mvc;

namespace NM_MultiSites.Areas.westhealth
{
    public class westhealthAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "westhealth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "westhealth_default",
                "westhealth/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}