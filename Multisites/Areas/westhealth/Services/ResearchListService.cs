using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IResearchListService
    {
        ResearchListViewModel GetResearchListViewModel();

    }
    public class ResearchListService : IResearchListService
    {
        public ResearchListService()
        {

        }

        public ResearchListViewModel GetResearchListViewModel()
        {
            {
                var model = new ResearchListViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.ResearchList.Fields.Title));
                    model.CardLocation = (ReferenceField)datasource.Fields[Templates.ResearchList.Fields.CardLocation];
                }

                Item folder = model.CardLocation.TargetItem;
                model.ResearchCards = GetResearchCardItems(folder); 

                return model;
            }
        }
        private List<ResearchCardViewModel> GetResearchCardItems(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.TemplateID.ToString() == (Templates.ResearchCard.TemplateID));
            if (children.Any())
            {
                return children.Select(x => MapResearchCardViewModel(x)).ToList();
            }
            return null;

        }
        private ResearchCardViewModel MapResearchCardViewModel(Item item)
        {
            var linkField = (LinkField)item.Fields[Templates.ResearchCard.Fields.CtaSource];
            return new ResearchCardViewModel
            {
                Title = new HtmlString(FieldRenderer.Render(item, Templates.ResearchCard.Fields.Title)),
                CtaSource = String.IsNullOrEmpty(item.Fields[Templates.ResearchCard.Fields.CtaSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(item.Fields[Templates.ResearchCard.Fields.CtaSource]),
                Source = new HtmlString(FieldRenderer.Render(item, Templates.ResearchCard.Fields.Source)),
                BackgroundColor = WestHealthSitecoreService.GetDroplinkValue(item.Fields[Templates.ResearchCard.Fields.BackgroundColor]),
                BackgroundCssClass = WestHealthSitecoreService.GetDroplinkValue(item.Fields[Templates.ResearchCard.Fields.BackgroundColor], Templates.ResearchCard.Fields.CssClass),
                CtaText = !string.IsNullOrWhiteSpace(linkField?.Text) ? linkField.Text : "Read More"

            };
        }
    }
}