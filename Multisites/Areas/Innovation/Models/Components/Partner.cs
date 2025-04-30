using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class Partner
    {
        public Item SourceItem { get; set; }
        public IHtmlString PartnerImage { get; set; }
        public IHtmlString CallOutBody { get; set; }
        public IHtmlString PartnerName { get; set; }
        public IHtmlString PartnerCompany { get; set; }
        public IHtmlString PartnerBioImage { get; set; }
        
    }
}