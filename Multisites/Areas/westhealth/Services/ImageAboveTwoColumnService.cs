using System;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IImageAboveTwoColumnService
    {
        ImageAboveTwoColumnViewModel GetImageAboveTwoColumnViewModel();

    }
    public class ImageAboveTwoColumnService : IImageAboveTwoColumnService
    {
        public ImageAboveTwoColumnService()
        {

        }

        public ImageAboveTwoColumnViewModel GetImageAboveTwoColumnViewModel()
        {
            var model = new ImageAboveTwoColumnViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.ImageAboveTwoColumn.Fields.Title));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.ImageAboveTwoColumn.Fields.Copy));
                model.CtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.ImageAboveTwoColumn.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.ImageAboveTwoColumn.Fields.CtaSource]);
                model.CardListTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.ImageAboveTwoColumn.Fields.CardListTitle));
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.ImageAboveTwoColumn.Fields.Image));
                model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
                if (String.IsNullOrEmpty(datasource.Fields[Templates.ImageAboveTwoColumn.Fields.CtaSource].GetValue(true)))
                {
                    model.CtaText = string.Empty;
                }
                else
                {
                    var linkField = (LinkField)datasource.Fields[Templates.ImageAboveTwoColumn.Fields.CtaSource];
                    model.CtaText = !string.IsNullOrWhiteSpace(linkField?.Text) ? linkField.Text : "Read More";
                }
                model.SectionId = datasource.Fields[Templates.ImageAboveTwoColumn.Fields.SectionId].GetValue(true) ?? string.Empty;
            }

            return model;
        }
    }

}