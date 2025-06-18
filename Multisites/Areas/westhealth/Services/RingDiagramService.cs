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
    public interface IRingDiagramService
    {
        RingDiagramViewModel GetRingDiagramViewModel();

    }
    public class RingDiagramService : IRingDiagramService
    {
        public RingDiagramService()
        {

        }

        public RingDiagramViewModel GetRingDiagramViewModel()
        {
            {
                var model = new RingDiagramViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                ReferenceField ringLocation; 
                if (datasource != null)
                {
                    //model.SourceItem = datasource;
                    //model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.RingDiagram.Fields.Title));
                    model.Title = datasource.Fields[Templates.RingDiagram.Fields.Title].Value; 
                    //model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.RingDiagram.Fields.Copy));
                    model.Copy = datasource.Fields[Templates.RingDiagram.Fields.Copy].Value;
                    ringLocation = (ReferenceField)datasource.Fields[Templates.RingDiagram.Fields.RingLocation];
                    Item folder = ringLocation.TargetItem;
                    model.Rings = GetRingItems(folder);
                }

                //Item folder = ringLocation.TargetItem;
                //model.Rings = GetRingItems(folder);

                return model;
            }
        }

        private List<RingViewModel> GetRingItems(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.TemplateID.ToString() == (Templates.RingDiagram.RingTemplateID));
            if (children.Any())
            {
                return children.Select(x => MapRingViewModel(x)).ToList();
            }
            return null;
        }

        private RingViewModel MapRingViewModel(Item item)
        {
            var cards = GetCardItems(item);

            return new RingViewModel
            {
                //Label = new HtmlString(FieldRenderer.Render(item, Templates.Ring.Fields.Label)),
                Label = item.Fields[Templates.Ring.Fields.Label].Value,
                //ShortLabel = new HtmlString(FieldRenderer.Render(item, Templates.Ring.Fields.ShortLabel)),
                ShortLabel = item.Fields[Templates.Ring.Fields.ShortLabel].Value,
                ThemeColor = WestHealthSitecoreService.GetDroplinkValue(item.Fields[Templates.Ring.Fields.ThemeColor]),
                ThemeCssColor = WestHealthSitecoreService.GetDroplinkValue(item.Fields[Templates.Ring.Fields.ThemeColor], Templates.Ring.Fields.ThemeCssColor),
                Cards = cards
            };
        }

        private List<RingCardViewModel> GetCardItems(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.TemplateID.ToString() == (Templates.Ring.CardTemplateID));
            if (children.Any())
            {
                return children.Select(x => MapRingCardViewModel(x)).ToList();
            }
            return null; 
        }

        private RingCardViewModel MapRingCardViewModel(Item item)
        {
            return new RingCardViewModel
            {
                CardTitle = item.Fields[Templates.Card.Fields.Title].Value,
                CardCopy = item.Fields[Templates.Card.Fields.Copy].Value,
            };
        }
    }
}