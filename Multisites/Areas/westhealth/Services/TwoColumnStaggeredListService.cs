using System;
using System.Collections.Generic;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ITwoColumnStaggeredListService
    {
       TwoColumnStaggeredListViewModel GetTwoColumnStaggeredListViewModel();

    }
    public class TwoColumnStaggeredListService : ITwoColumnStaggeredListService
    {
        public TwoColumnStaggeredListService()
        {

        }

        public TwoColumnStaggeredListViewModel GetTwoColumnStaggeredListViewModel()
        {
            var model = new TwoColumnStaggeredListViewModel();
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.Title));
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.Image));
                model.ProviderTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.ProviderTitle));
                model.ProviderList = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.ProviderList));
                model.ProviderString = datasource[Templates.TwoColumnStaggeredList.Fields.ProviderList];
                model.PatientTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.PatientTitle));
                model.PatientList = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.PatientList));
                model.PatientString = datasource[Templates.TwoColumnStaggeredList.Fields.PatientList];
                model.ListBackgroundColor = GetDroplinkValue(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.ListBackgroundColor]);
                model.ProviderCtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.ProviderCtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.ProviderCtaSource]);
                model.PatientCtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.PatientCtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.PatientCtaSource]);

            }
            model.ProviderBullets = GetBullets(model.ProviderString);
            model.PatientBullets = GetBullets(model.PatientString); 


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
        private List<string> GetBullets(string bulletString)
        {
            var bullets = bulletString.Split(new char[] { '\n' },  StringSplitOptions.RemoveEmptyEntries); 
            return new List<string>(bullets);
        }
    }
}