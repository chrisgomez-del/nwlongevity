using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ISplitContentHeroService
    {
        SplitContentHeroViewModel GetSplitContentHeroViewModel();

    }
    public class SplitContentHeroService : ISplitContentHeroService
    {
        public SplitContentHeroService()
        {

        }

        public SplitContentHeroViewModel GetSplitContentHeroViewModel()
        {
            {
                var model = new SplitContentHeroViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.SplitContentHero.Fields.Image));
                    model.ImageLocation = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentHero.Fields.ImageLocation]);
                    model.ImageLocationCssClass = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentHero.Fields.ImageLocation], Templates.SplitContentHero.Fields.CssClass);
                    model.BackgroundColor = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentHero.Fields.BackgroundColor]);
                    model.BackgroundCssClass = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentHero.Fields.BackgroundColor], Templates.SplitContentHero.Fields.CssClass);
                    model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                    model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);                    

                }
                return model;
            }
        }
    }
}