using System;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ICardService
    {
        CardViewModel GetCardViewModel();
    }

    public class CardService : ICardService
    {
        public CardViewModel GetCardViewModel()
        {
            var model = new CardViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Title));
                model.SubTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.SubTitle));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Copy));
                model.CtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.Card.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.Card.Fields.CtaSource]);
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Image));
                model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
                if (String.IsNullOrEmpty(datasource.Fields[Templates.Card.Fields.CtaSource].GetValue(true)))
                {
                    model.CtaText = string.Empty;
                } else
                {
                    var linkField = (LinkField)datasource.Fields[Templates.Card.Fields.CtaSource];
                    model.CtaText = !string.IsNullOrWhiteSpace(linkField?.Text) ? linkField.Text : "Read More";                    
                }

            }
            return model;
        }
    }
}