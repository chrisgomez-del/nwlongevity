using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IThreeCardCalloutService
    {
        ThreeCardCalloutViewModel GetThreeCardCalloutViewModel();

    }
    public class ThreeCardCalloutService : IThreeCardCalloutService
    {
        public ThreeCardCalloutService()
        {

        }

        public ThreeCardCalloutViewModel GetThreeCardCalloutViewModel()
        {
            var model = new ThreeCardCalloutViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.ThreeCardCallout.Fields.Title));
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.ThreeCardCallout.Fields.Image));
                model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
            }
            return model;
        }
    }
}