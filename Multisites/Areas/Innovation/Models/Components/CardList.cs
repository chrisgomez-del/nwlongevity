using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class CardList
    {
        public CardList()
        {
            Collection = new List<Card>();
        }
        public HtmlString Title { get; set; }
        public HtmlString Description { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundClass { get; set; }
        public List<Card> Collection { get; set; }
        
    }
}