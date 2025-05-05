using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class FolderGroup
    {
        public string parentId { get; set; }
        public string Name { get; set; }
        public Item Item { get; set; }
        public int Order { get; set; }
        public int Level { get; set; }
        public string Type { get; set; }
        public bool hasSubitems { get; set; }
        public List<FolderGroup> Subitems { get; set; }

        public FolderGroup()
        {
            Subitems = new List<FolderGroup>();
        }
    }
}