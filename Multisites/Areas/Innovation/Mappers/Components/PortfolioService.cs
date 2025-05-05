using NM_MultiSites.Areas.Innovation.Data;
using NM_MultiSites.Areas.Innovation.Helpers;
using NM_MultiSites.Areas.Innovation.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Mappers.Components
{
    public interface IPortfolioService
    {
        ImageTextCard GetImageTextCardData();
        MultiImageTextCard GetMultiImageTextsectionCardData();
        CaseStudyPagination GetPagenationData();
        CaseStudyList GetCaseStudyData();
        Partner GetPartnerData();
    }
    public class PortfolioService : IPortfolioService
    {
        public ImageTextCard GetImageTextCardData()
        {
            ImageTextCard data = new ImageTextCard();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                var lf = datasource.Fields["Link"];
                data.SourceItem = datasource;
                data.Image = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.BackgroundImage = SitecoreAccess.GetMediaUrl(datasource, "BackgroundImage");
                data.Link = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.LinkTitle = new HtmlString(FieldRenderer.Render(datasource, "LinkTitle"));

            }
            return data;
        }

        public MultiImageTextCard GetMultiImageTextsectionCardData()
        {
            MultiImageTextCard data = new MultiImageTextCard();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                var lf_vs = datasource.Fields["VerticalSection Link"];
                var lf_hs = datasource.Fields["HorizontalSection Link"];
                var lf_bg = datasource.Fields["HorizontalSection BackgroundImage"];
                var lf_vb = datasource.Fields["VerticalSection BackgroundImage"];
                data.SourceItem = datasource;
                data.VerticalSection_Title = new HtmlString(FieldRenderer.Render(datasource, "VerticalSection Title"));
                data.VerticalSection_BackgroundImage = String.IsNullOrEmpty(lf_vb.GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource, "VerticalSection BackgroundImage");
                data.VerticalSection_Link = String.IsNullOrEmpty(lf_vs.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf_vs);
                data.VerticalSection_LinkTitle = new HtmlString(FieldRenderer.Render(datasource, "VerticalSection LinkTitle"));
                data.HorizontalSection_Title = new HtmlString(FieldRenderer.Render(datasource, "HorizontalSection Title"));
                data.HorizontalSection_BackgroundImage = String.IsNullOrEmpty(lf_bg.GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource, "HorizontalSection BackgroundImage");
                data.HorizontalSection_Link = String.IsNullOrEmpty(lf_hs.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf_hs);
                data.HorizontalSection_LinkTitle = new HtmlString(FieldRenderer.Render(datasource, "HorizontalSection LinkTitle"));
                data.FirstImage = new HtmlString(FieldRenderer.Render(datasource, "First Image"));
                data.SecondImage = new HtmlString(FieldRenderer.Render(datasource, "Second Image"));

            }
            return data;
        }

        public CaseStudyPagination GetPagenationData()
        {
            CaseStudyPagination data = new CaseStudyPagination()
            {
                ShowonPage = false
            };
            Item currentItem = Sitecore.Context.Item;
            if (currentItem != null && SitecoreAccess.InheritsFrom(currentItem, Templates.CaseStudyPageId))
            {
                data.ShowonPage = true;
                var prev = Sitecore.Context.Item.Axes.SelectItems("./preceding::*[@@templateid='{D333468D-8609-482D-8993-786713EB6A07}']");
                Item previous = prev != null ? prev.LastOrDefault() : null;
                Item next = Sitecore.Context.Item.Axes.SelectSingleItem("./following::*[@@templateid='{D333468D-8609-482D-8993-786713EB6A07}']");
                Item Parent = currentItem.Parent;

                data.PreviousUrl = previous != null ? Sitecore.Links.LinkManager.GetItemUrl(previous) : string.Empty;
                data.NextUrl = next != null ? Sitecore.Links.LinkManager.GetItemUrl(next) : string.Empty;
                data.PortfolioUrl = Parent != null ? Sitecore.Links.LinkManager.GetItemUrl(Parent) : string.Empty;
            }
            return data;
        }

        public CaseStudyList GetCaseStudyData()
        {
            CaseStudyList Container = new CaseStudyList();
            Item Datasource = SitecoreAccess.GetDataSourceItem();
            if (Datasource != null)
            {
                Container.Title = new HtmlString(FieldRenderer.Render(Datasource, "Title"));
                List<Item> CaseStudies = SitecoreAccess.GetMultiListItems(Datasource, "Case Studies");
                if (CaseStudies != null)
                {

                    foreach (Item child in CaseStudies)
                    {

                        CaseStudy ChildCard = new CaseStudy()
                        {
                            BackgroundImage = SitecoreAccess.GetMediaUrl(child, "Background Image"),
                            Title = new HtmlString(FieldRenderer.Render(child, "Navigation Title")),
                            CTATitle = new HtmlString(FieldRenderer.Render(child, "CTA Title")),
                            Link = Sitecore.Links.LinkManager.GetItemUrl(child),
                            SourceItem = child
                        };
                        Container.CaseStudies.Add(ChildCard);

                    }


                }
            }
            return Container;
        }

        public Partner GetPartnerData()
        {
            Partner data = new Partner();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.PartnerImage = new HtmlString(FieldRenderer.Render(datasource, "PartnerImage"));
                data.CallOutBody = new HtmlString(FieldRenderer.Render(datasource, "CallOutBody"));
                data.PartnerBioImage = new HtmlString(FieldRenderer.Render(datasource, "PartnerBioImage"));
                data.PartnerName = new HtmlString(FieldRenderer.Render(datasource, "PartnerName"));
                data.PartnerCompany = new HtmlString(FieldRenderer.Render(datasource, "PartnerCompany"));
            }
            return data;
        }
    }
}