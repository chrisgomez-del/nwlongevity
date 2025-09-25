using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.longevity;
using Sitecore.CodeDom.Scripts;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.longevity.Helpers
{
    
    public class Head
    {
        private const string METADATATITLE = "Title";
        private const string METADATADESCRIPTION = "Description";
        private const string METADATAKEYWORDS = "Keywords";
        private const string METADATAROBOTS = "Robots";
        private const string METADATAAUTHOR = "Author";

        private const string OGTITLE = "OG Title";
        private const string OGDESCRIPTION = "OG Description";
        private const string OGTYPE = "OG Type";
        private const string OGIMAGE = "OG Image";
        private const string OGSITENAME = "OG Site Name";

        private const string TWITTERTITLE = "Twitter Title";
        private const string TWITTERDESCRIPTION = "Twitter Description";
        private const string TWITTERIMAGE = "Twitter Image";

        public static Models.Head GetTags(Item item)
        {
            
            Models.Head head = new Models.Head();

            head.Metadata.Title = SitecoreAccess.HasValue(item, METADATATITLE) ? item.Fields[METADATATITLE].Value : string.Empty;
            head.Metadata.Description = SitecoreAccess.HasValue(item, METADATADESCRIPTION) ? item.Fields[METADATADESCRIPTION].Value : string.Empty;
            head.Metadata.Keywords = SitecoreAccess.HasValue(item, METADATAKEYWORDS) ? item.Fields[METADATAKEYWORDS].Value : string.Empty;
            head.Metadata.Robots = SitecoreAccess.HasValue(item, METADATAROBOTS) ? item.Fields[METADATAROBOTS].Value : string.Empty;
            head.Metadata.Author = SitecoreAccess.HasValue(item, METADATAAUTHOR) ? item.Fields[METADATAAUTHOR].Value : string.Empty;

            head.OGTags.Title = SitecoreAccess.HasValue(item, OGTITLE) ? item.Fields[OGTITLE].Value : string.Empty;
            head.OGTags.Description = SitecoreAccess.HasValue(item, OGDESCRIPTION) ? item.Fields[OGDESCRIPTION].Value : string.Empty;
            head.OGTags.Type = SitecoreAccess.HasValue(item, OGTYPE) ? item.Fields[OGTYPE].Value : string.Empty;
            head.OGTags.Image = SitecoreAccess.HasValue(item, OGIMAGE) ? item.Fields[OGIMAGE].Value : string.Empty;
            head.OGTags.SiteName = SitecoreAccess.HasValue(item, OGSITENAME) ? item.Fields[OGSITENAME].Value : string.Empty;

            head.Twitter.Title = SitecoreAccess.HasValue(item, TWITTERTITLE) ? item.Fields[TWITTERTITLE].Value : string.Empty;
            head.Twitter.Description = SitecoreAccess.HasValue(item, TWITTERDESCRIPTION) ? item.Fields[TWITTERDESCRIPTION].Value : string.Empty;
            head.Twitter.Image = SitecoreAccess.HasValue(item, TWITTERIMAGE) ? item.Fields[TWITTERIMAGE].Value : string.Empty;

            return head;
        }
    }
}