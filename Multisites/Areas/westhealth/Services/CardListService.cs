using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ICardListService
    {
        CardListViewModel GetCardListViewModel();

    }
    public class CardListService : ICardListService
    {
        public CardListService()
        {

        }

        public CardListViewModel GetCardListViewModel()
        {
            {
                var model = new CardListViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.CardList.Fields.Title));
                    model.SectionId = datasource.Fields[Templates.CardList.Fields.SectionId].GetValue(true) ?? string.Empty;
                }

                return model;
            }
        }
    }
}