using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class CardListViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public string SectionId { get; set; }
    }
}