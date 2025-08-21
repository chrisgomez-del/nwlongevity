using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class TabResourceViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Copy { get; set; }
        public string PdfSource { get; set; }
        public string PdfText { get; set; }
        public string CtaSource { get; set; }
        public string CtaText { get; set; }
    }
}