using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.longevity.Models
{
    public class Head
    {
        public Head() {
            Metadata = new Metadata();
            OGTags = new OG();
            Twitter = new Twitter();
        }
        public Metadata Metadata { get; set; }
        public OG OGTags { get; set; }
        public Twitter Twitter { get; set; }
    }

    public class Metadata
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Robots { get; set; }
        public string Author { get; set; }
    }

    public class OG
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string SiteName { get; set; }

    }

    public class Twitter
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}