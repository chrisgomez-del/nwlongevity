using System.Web;
using NM_MultiSites.Areas.Innovation.Models.Components;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IAccordionPanelService
    {
        AccordionPanelViewModel GetAccordionPanelData();
        AccordionItemViewModel GetAccordionItemData();
    }
    public class AccordionPanelService : IAccordionPanelService
    {
        public AccordionPanelViewModel GetAccordionPanelData()
        {
            var data = new AccordionPanelViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.AccordionPanel.Fields.Title));
            }
            return data;
        }
        public AccordionItemViewModel GetAccordionItemData()
        {
            var data = new AccordionItemViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.AccordionItem.Fields.Title));
                data.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.AccordionItem.Fields.Copy));
                data.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.AccordionItem.Fields.Image));
            }
            return data;
        }
    }
}