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
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.Title));
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.Image));
                model.LeftListTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.LeftListTitle));
                model.LeftListTab = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.LeftListTab));
                model.LeftList = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.LeftList));
                model.LeftListString = datasource[Templates.TwoColumnStaggeredList.Fields.LeftList];
                model.RightListTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.RightListTitle));
                model.RightListTab = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.RightListTab));
                model.RightList = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.RightList));
                model.RightListString = datasource[Templates.TwoColumnStaggeredList.Fields.RightList];
                model.LeftListCtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.LeftListCtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.LeftListCtaSource]);
                var leftLinkField = (LinkField)datasource.Fields[Templates.TwoColumnStaggeredList.Fields.LeftListCtaSource];
                model.LeftListCtaText = !string.IsNullOrWhiteSpace(leftLinkField?.Text) ? leftLinkField.Text : "Download PDF"; 
                model.RightListCtaSource = String.IsNullOrEmpty(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.RightListCtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(datasource.Fields[Templates.TwoColumnStaggeredList.Fields.RightListCtaSource]);
                var rightLinkField = (LinkField)datasource.Fields[Templates.TwoColumnStaggeredList.Fields.RightListCtaSource];
                model.RightListCtaText = !string.IsNullOrWhiteSpace(rightLinkField?.Text) ? rightLinkField.Text : "Download PDF";
                model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);

            }
            model.LeftListBullets = GetBullets(model.LeftListString);
            model.RightListBullets = GetBullets(model.RightListString); 


            return model;
        }
        private List<string> GetBullets(string bulletString)
        {
            var bullets = bulletString.Split(new char[] { '\n' },  StringSplitOptions.RemoveEmptyEntries); 
            return new List<string>(bullets);
        }
    }
}