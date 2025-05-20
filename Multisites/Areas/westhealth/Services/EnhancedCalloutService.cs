using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IEnhancedCalloutService
    {
        EnhancedCalloutViewModel GetEnhancedCalloutViewModel();

    }
    public class EnhancedCalloutService : IEnhancedCalloutService
    {
        private readonly ICardService _cardService;

        public EnhancedCalloutService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public EnhancedCalloutViewModel GetEnhancedCalloutViewModel()
        {
            var model = new EnhancedCalloutViewModel();
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.EnhancedCallout.Fields.Title));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.EnhancedCallout.Fields.Copy));

            }
            var children = datasource.GetChildren();
            model.Cards = new List<CardViewModel>();
            foreach (Item child in children)
            {
                model.Cards.Add(_cardService.GetCardViewModel(child.ID));
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