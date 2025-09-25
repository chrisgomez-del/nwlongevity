using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IFlippingCardsService
    {
        FlippingCards GetFlippingCardsData();

    }
    public class FlippingCardsService : IFlippingCardsService
    {
        public FlippingCards GetFlippingCardsData()
        {
            FlippingCards flippingCards = new FlippingCards();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                foreach (Sitecore.Data.Items.Item child in datasource.Children)
                {
                    FlippingCard card = new FlippingCard();
                    card.ImagePath = String.IsNullOrEmpty(child.Fields["Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Image");
                    card.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                    card.TopText = new HtmlString(FieldRenderer.Render(child, "Top Text"));
                    card.MiddleText = new HtmlString(FieldRenderer.Render(child, "Middle Text"));
                    card.BottomText = new HtmlString(FieldRenderer.Render(child, "Bottom Text"));
                    flippingCards.FlippingCardCollection.Add(card);
                }
            }
            return flippingCards;
        }

    }
}
