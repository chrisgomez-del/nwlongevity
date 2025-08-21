using System.Web.Mvc;

namespace NM_MultiSites.Areas.longevity
{
    public class longevityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "longevity";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "longevity_default",
                "longevity/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}