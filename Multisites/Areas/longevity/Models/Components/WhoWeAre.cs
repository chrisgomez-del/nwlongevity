using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class WhoWeAre
    {
        public WhoWeAre()
        {
            SectionCollection = new List<WhoWeAreSection>();
        }
        public List<WhoWeAreSection> SectionCollection { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString BottomContent { get; set; }
        
    }

    public class WhoWeAreSection
    {
        public WhoWeAreSection()
        {
            RightTeamMembers = new List<TeamMember>();
            RightTimelineEvents = new List<TimelineEvent>();
        }
        public string Class { get; set; }
        public HtmlString LeftDescription { get; set; }
        public GeneralLink LeftCTA { get; set; }
        public HtmlString RightContent { get; set; }
        public HtmlString RightComponent { get; set; }
        public string RightComponentDatasource { get; set; }
        public List<TeamMember> RightTeamMembers { get; set; }
        public List<TimelineEvent> RightTimelineEvents { get; set; }
    }
}