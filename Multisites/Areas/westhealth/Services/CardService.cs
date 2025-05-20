using System;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ICardService
    {
        CardViewModel GetCardViewModel();
        CardViewModel GetCardViewModel(ID id);

    }
    public class CardService : ICardService
    {
        public CardViewModel GetCardViewModel()
        {
            var model = new CardViewModel();
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Title));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Copy));
                model.CtaText = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.CtaText));
                model.CtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.Card.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.Card.Fields.CtaSource]);
            }
            return model;
        }

        public CardViewModel GetCardViewModel(ID id)
        {
            var model = new CardViewModel();
            Item datasource = GetItemById(id);
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Title));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.Copy));
                model.CtaText = new HtmlString(FieldRenderer.Render(datasource, Templates.Card.Fields.CtaText));
                model.CtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.Card.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.Card.Fields.CtaSource]);
            }
            return model;
        }

        public Item GetDataSourceItem()
        {
            var dataSourceId = RenderingContext.CurrentOrNull.Rendering.DataSource;
            return GetItemById(dataSourceId);
        }

        public Item GetItemById(ID id)
        {
            return Sitecore.Context.Database.GetItem(id);
        }
        public Item GetItemById(string path)
        {
            return Sitecore.Context.Database.GetItem(path);
        }
    }
}