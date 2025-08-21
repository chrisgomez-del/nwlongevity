using System;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ITwoColumnImageStackService
    {
        TwoColumnImageStackViewModel GetTwoImageStackViewModel();

    }
    public class TwoColumnImageStackService : ITwoColumnImageStackService
    {
        public TwoColumnImageStackService()
        {

        }

        public TwoColumnImageStackViewModel GetTwoImageStackViewModel()
        {
            var model = new TwoColumnImageStackViewModel();
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.TopImage = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnImageStack.Fields.TopImage));
                model.TopBackground = GetDroplinkValue(datasource.Fields[Templates.TwoColumnImageStack.Fields.TopBackground]);
                model.BottomImage = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnImageStack.Fields.BottomImage));
                model.BottomBackground = GetDroplinkValue(datasource.Fields[Templates.TwoColumnImageStack.Fields.BottomBackground]);
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

        private string GetDroplinkValue(ReferenceField referenceField)
        {
            if (referenceField == null)
            {
                return string.Empty;
            }
            else if (referenceField.TargetItem == null)
            {
                return string.Empty;
            }
            else
            {
                return referenceField.TargetItem.Name;
            }
        }
    }
}