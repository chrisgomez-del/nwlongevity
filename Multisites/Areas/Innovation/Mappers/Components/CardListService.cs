using Innovation.Areas.Innovation.Models.Components;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Innovation.Areas.Innovation.Helpers;
using Sitecore.Data.Fields;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System.Globalization;

namespace Innovation.Areas.Innovation.Mappers.Components
{
    public interface ICardListService
    {
        EventList GetEventListData();
        Card GetCardData();
        TextBlock GetAccordionData();

        NewsFeedCard GetNewsFeedData();
    }
    public class CardListService : ICardListService
    {
        public TextBlock GetAccordionData()
        {
            TextBlock data = new TextBlock();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));

            }
            return data;
        }

        public Card GetCardData()
        {
            Card data = new Card();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                var lf = datasource.Fields["Link"];
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.BackgroundImage = SitecoreAccess.GetMediaUrl(datasource, "BackgroundImage");
                data.Link = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.LinkTitle = new HtmlString(FieldRenderer.Render(datasource, "LinkTitle"));

            }
            return data;
        }

        public EventList GetEventListData() 
        {
            EventList Container = new EventList();
            Item Datasource = SitecoreAccess.GetDataSourceItem();
            if(Datasource!= null)
            {
                Container.Title = new HtmlString(FieldRenderer.Render(Datasource, "Title"));
                Container.Description = new HtmlString(FieldRenderer.Render(Datasource, "Description"));
                Container.BackgroundImage = SitecoreAccess.GetMediaUrl(Datasource,"Background Image");
                Container.BackgroundClass = FieldRenderer.Render(Datasource, "Background Class");
                string ContainerSource = Datasource.Fields["Cards Source"].Value;
                Item ContainerSourceItem = SitecoreAccess.GetItemById(ContainerSource);
                if(ContainerSourceItem!=null)
                {
                    var childitems = SitecoreAccess.GetChildItems(ContainerSourceItem);
                    if(childitems!=null)
                    {
                        foreach(Item child in childitems)
                        {
                            LinkField lf = child.Fields["Link"];
                            Event ChildCard = new Event() {
                                Type = new HtmlString(FieldRenderer.Render(child, "Type")),
                                Title = new HtmlString(FieldRenderer.Render(child, "Title")),
                                LinkTitle = lf!=null ? lf.Text : String.Empty,
                                Link = String.IsNullOrEmpty(child.Fields["Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf),
                                DateFrom = GetDateFieldValue(child, "Event Start Date", "yyyy-MM-dd"),
                                DateTo  = GetDateFieldValue(child, "Event End Date", "yyyy-MM-dd"),
                                DateString = new HtmlString(FieldRenderer.Render(child, "Date String")),
                                Location = new HtmlString(FieldRenderer.Render(child, "Location"))
                            };
                            
                            DateTime Eventenddate = String.IsNullOrWhiteSpace(ChildCard.DateTo)?
                                                    DateTime.MinValue : 
                                                    Convert.ToDateTime(ChildCard.DateTo).Date;

                            if(Eventenddate.Date >= DateTime.Now.Date)
                            {
                                Container.Events.Add(ChildCard);
                            }
                            
                        }
                        Container.Events.OrderBy(x => x.DateFrom);
                    }
                }
            }
            return Container;
        }

        public NewsFeedCard GetNewsFeedData()
        {
            NewsFeedCard data = new NewsFeedCard();
            Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                var lf = datasource.Fields["CTALink"];
                data.SourceItem = datasource;
                data.Heading = new HtmlString(FieldRenderer.Render(datasource, "Heading"));
                data.SubHeading = new HtmlString(FieldRenderer.Render(datasource, "SubHeading"));
                data.CTALink = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.CTAText = new HtmlString(FieldRenderer.Render(datasource, "CTAText"));

            }
            return data;

        }
        private string GetDateFieldValue(Item item, string datefield , string format)
        {
            string formattedDateValue = String.Empty;
            Field fc = item.Fields[datefield];
            if (fc != null && !String.IsNullOrWhiteSpace(fc.GetValue(true)))
            {
                DateField df = (DateField)fc;
                DateTime dt = df.DateTime;
                if (dt != null)
                return dt.ToString(format);
            }
            return formattedDateValue;
        }
    }
}
