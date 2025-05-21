using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
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
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnWithImage.Fields.Image));
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