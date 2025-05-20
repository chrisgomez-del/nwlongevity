using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class TeamMemberViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Name { get; set; }
        public IHtmlString Qualifications { get; set; }
        public IHtmlString Titles { get; set; }
        public IHtmlString Image { get; set; }
        public IHtmlString ProfileLink { get; set; }
    }
}