using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface INavigableTabsService
    {
        NavigableTabsViewModel GetNavigableTabsViewModel();

    }
    public class NavigableTabsService : INavigableTabsService
    {
        public NavigableTabsService()
        {

        }

        public NavigableTabsViewModel GetNavigableTabsViewModel()
        {
            {
                var model = new NavigableTabsViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.NavigableTabs.Fields.Title));
                    model.NavigableTabsLocation = (ReferenceField)datasource.Fields[Templates.NavigableTabs.Fields.NavigableTabsLocation];
                    model.SectionId = datasource.Fields[Templates.NavigableTabs.Fields.SectionId].GetValue(true) ?? string.Empty;
                }

                Item folder = model.NavigableTabsLocation.TargetItem;
                if (folder != null)
                {
                    model.NavigableTabs = GetNavigableTabItems(folder);
                }
                else
                {
                    model.NavigableTabs = new List<NavigableTabViewModel>();
                }

                return model;
            }
        }
        private List<NavigableTabViewModel> GetNavigableTabItems(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.TemplateID.ToString() == (Templates.NavigableTabs.TemplateID));
            if (children.Any())
            {
                return children.Select(x => MapNavigableTabViewModel(x)).ToList();
            }
            return null;

        }
        private NavigableTabViewModel MapNavigableTabViewModel(Item item)
        {
            List<TabResourceViewModel> tabResources = GetTabResources(item);

            return new NavigableTabViewModel
            {
                Title = new HtmlString(FieldRenderer.Render(item, Templates.NavigableTab.Fields.Title)),
                TabId = Regex.Replace(FieldRenderer.Render(item, Templates.NavigableTab.Fields.Title), "[^a-zA-Z0-9]", ""),
                TabResources = tabResources,
            };

        }
        private List<TabResourceViewModel> GetTabResources(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.TemplateID.ToString() == (Templates.NavigableTab.TemplateID));
            if (children.Any())
            {
                return children.Select(x => MapTabResourceViewModel(x)).ToList();
            }
            return null;

        }
        private TabResourceViewModel MapTabResourceViewModel(Item item)
        {
            var pdfLinkField = (LinkField)item.Fields[Templates.TabResource.Fields.PdfSource];
            var ctaLinkField = (LinkField)item.Fields[Templates.TabResource.Fields.CtaSource];

            return new TabResourceViewModel
            {
                Title = new HtmlString(FieldRenderer.Render(item, Templates.TabResource.Fields.Title)),
                Copy = new HtmlString(FieldRenderer.Render(item, Templates.TabResource.Fields.Copy)),
                PdfSource = String.IsNullOrEmpty(item.Fields[Templates.TabResource.Fields.PdfSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(item.Fields[Templates.TabResource.Fields.PdfSource]),
                PdfText = !string.IsNullOrWhiteSpace(pdfLinkField?.Text) ? pdfLinkField.Text : "Download PDF",
                CtaSource = String.IsNullOrEmpty(item.Fields[Templates.TabResource.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(item.Fields[Templates.TabResource.Fields.CtaSource]),
                CtaText = !string.IsNullOrWhiteSpace(ctaLinkField?.Text) ? ctaLinkField.Text : "Learn More",
            };

        }
    }
}