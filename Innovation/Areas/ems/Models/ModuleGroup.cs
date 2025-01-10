using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.ems.Models
{
    public class ModuleGroup:Module
    {
        public ModuleGroup() { }
        public HtmlString GroupTitle { get; set; }
        public String GroupId { get; set; }

        public int ColumnStyle { get; set; }

        public IEnumerable<Module> modules{ get; set; }
    }
}