using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ISplitContentSubtitleService
    {
        SplitContentSubtitleViewModel GetSplitContentSubtitleViewModel();

    }
    public class SplitContentSubtitleService : ISplitContentSubtitleService
    {
        public SplitContentSubtitleService()
        {

        }

        public SplitContentSubtitleViewModel GetSplitContentSubtitleViewModel()
        {
            {
                var model = new SplitContentSubtitleViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.SplitContentSubtitle.Fields.Image));
                    model.ImageLocation = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentSubtitle.Fields.ImageLocation]);
                    model.ImageLocationCssClass = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentSubtitle.Fields.ImageLocation], Templates.SplitContentSubtitle.Fields.CssClass);
                    model.BackgroundColor = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentSubtitle.Fields.BackgroundColor]);
                    model.BackgroundCssClass = WestHealthSitecoreService.GetDroplinkValue(datasource.Fields[Templates.SplitContentSubtitle.Fields.BackgroundColor], Templates.SplitContentSubtitle.Fields.CssClass);
                    model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                    model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
                }

                return model;
            }
        }
    }
}