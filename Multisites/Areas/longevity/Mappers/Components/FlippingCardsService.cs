using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;
using NM_MultiSites.Areas.Longevity.Models;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IFlippingCardsService
    {
        FlippingCards GetFlippingCardsData();

    }
    public class FlippingCardsService : IFlippingCardsService
    {
        public const string FLIPPINGCARDTEMPLATEID = "{195BE226-920A-4209-8741-02EEFAA8B984}";

        public FlippingCards GetFlippingCardsData()
        {
            FlippingCards flippingCards = new FlippingCards();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                ChildList children = datasource.GetChildren();
                foreach (Sitecore.Data.Items.Item child in children)
                {
                    FlippingCard card = new FlippingCard();


                    if (child.TemplateID.ToString() == FLIPPINGCARDTEMPLATEID)
                    {
                        card.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                        card.CTADescription = new HtmlString(FieldRenderer.Render(child, "Description"));

                        GeneralLink cta = new GeneralLink()
                        {
                            Title = new HtmlString(SitecoreAccess.LinkTitle(child.Fields["CTA"])),
                            CTALink = String.IsNullOrEmpty(child.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(child.Fields["CTA"]),
                        };

                        card.CTA = cta;

                        card.IsCTACard = true;
                    }
                    else {
                        card.ImagePath = String.IsNullOrEmpty(child.Fields["Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Image");
                        card.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                        card.TopText = new HtmlString(FieldRenderer.Render(child, "Top Text"));
                        card.MiddleText = new HtmlString(FieldRenderer.Render(child, "Middle Text"));
                        card.BottomText = new HtmlString(FieldRenderer.Render(child, "Bottom Text"));

                        card.IsCTACard = false;
                    }
                    flippingCards.FlippingCardCollection.Add(card);
                }
            }
            return flippingCards;
        }

    }
}
