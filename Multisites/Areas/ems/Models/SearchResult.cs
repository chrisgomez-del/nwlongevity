using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class SearchResult : List<SearchItem>
    {
        public SearchResult()
        {

        }
        public String SearchTerm { get; set; }
        public Boolean DataAvailable { get; set; }  
    }
}