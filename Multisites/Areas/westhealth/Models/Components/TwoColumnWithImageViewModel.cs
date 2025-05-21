using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class TwoColumnWithImageViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Image { get; set; }
        public string ImageLocation { get; set; }
    }
}