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
        public IHtmlString ProviderTitle { get; set; }
        public IHtmlString ProviderList { get; set; }
        public IHtmlString PatientTitle { get; set; }
        public IHtmlString PatientList { get; set; }
        public IHtmlString Image { get; set; }
        public string ListBackgroundColor { get; set; }
        public string ProviderString { get; set; }
        public string PatientString { get; set; }
        public List<string> ProviderBullets { get; set; }   
        public List<string> PatientBullets { get; set; }
        public string ProviderCtaSource { get; set; }
        public string PatientCtaSource { get; set; }
    }
}