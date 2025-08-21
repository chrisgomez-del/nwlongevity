using System.Text.RegularExpressions;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
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
                data.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.AccordionPanel.Fields.Image));
                data.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                data.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
                data.SectionId = datasource.Fields[Templates.AccordionPanel.Fields.SectionId].GetValue(true) ?? string.Empty;
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
                data.CardId = Regex.Replace(FieldRenderer.Render(datasource, Templates.AccordionItem.Fields.Title), "[^a-zA-Z]", ""); 
            }
            return data;
        }
    }
}