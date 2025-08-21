using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class TwoColumnImageStackViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString TopImage { get; set; }
        public string TopBackground { get; set; }
        public IHtmlString BottomImage { get; set; }
        public string BottomBackground { get; set; }
    }
}