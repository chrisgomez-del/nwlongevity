using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ITwoColumnWithImageService
    {
        TwoColumnWithImageViewModel GetTwoColumnWithImageViewModel();

    }
    public class TwoColumnWithImageService : ITwoColumnWithImageService
    {
        public TwoColumnWithImageService()
        {

        }

        public TwoColumnWithImageViewModel GetTwoColumnWithImageViewModel()
        {
            var model = new TwoColumnWithImageViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnWithImage.Fields.Image));
                model.ImageLocation = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.TwoColumnWithImage.Fields.ImageLocation]);
                model.ImageLocationCssClass = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentHero.Fields.ImageLocation], Templates.SplitContentHero.Fields.CssClass);
                model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
            }

            return model;
        }
    }
}