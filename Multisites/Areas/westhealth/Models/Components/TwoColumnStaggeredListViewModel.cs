using System.Collections.Generic;
using System.Web;
using Microsoft.SqlServer.Server;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class TwoColumnStaggeredListViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString LeftListTitle { get; set; }
        public IHtmlString LeftListTab { get; set; }
        public IHtmlString LeftList { get; set; }
        public IHtmlString RightListTitle { get; set; }
        public IHtmlString RightListTab { get; set; }
        public IHtmlString RightList { get; set; }
        public IHtmlString Image { get; set; }
        public string LeftListString { get; set; }
        public string RightListString { get; set; }
        public List<string> LeftListBullets { get; set; }   
        public List<string> RightListBullets { get; set; }
        public string LeftListCtaSource { get; set; }
        public string LeftListCtaText { get; set; }
        public string RightListCtaSource { get; set; }
        public string RightListCtaText { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string SectionId { get; set; }
        public string LeftListCtaTarget { get; set; }
        public string RightListCtaTarget { get; set; }
    }
}