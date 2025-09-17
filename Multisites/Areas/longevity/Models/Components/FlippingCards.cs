using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class FlippingCards
    {
        public FlippingCards()
        {
            FlippingCardCollection = new List<FlippingCard>();
        }

        public List<FlippingCard> FlippingCardCollection { get; set; }

    }

    public class FlippingCard
    {
        public string ImagePath { get; set; }
        public HtmlString TopText { get; set; }
        public HtmlString MiddleText { get; set; }
        public HtmlString BottomText { get; set; }
    }
}