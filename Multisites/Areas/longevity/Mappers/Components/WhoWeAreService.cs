using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Common;
using static NM_MultiSites.Areas.westhealth.Templates;
using TeamMember = NM_MultiSites.Areas.Longevity.Models.Components.TeamMember;
using NM_MultiSites.Areas.Longevity.Models;
using System.Collections.Generic;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;
using Sitecore.Collections;
using Sitecore.Data;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IWhoWeAreService
    {
        WhoWeAre GetWhoWeAreData();

    }
    public class WhoWeAreService : IWhoWeAreService
    {
        public WhoWeAre GetWhoWeAreData()
        {
            WhoWeAre whoWeAre = new WhoWeAre();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                bool isMobile = HttpContext.Current?.Request?.Browser?.IsMobileDevice ?? false;

                whoWeAre.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                whoWeAre.BottomContent = new HtmlString(FieldRenderer.Render(datasource, "Bottom Content"));

                ChildList children = datasource.GetChildren();
                foreach (Sitecore.Data.Items.Item child in children)
                {
                    WhoWeAreSection whoWeAreSection = new WhoWeAreSection();
                    whoWeAreSection.LeftDescription = new HtmlString(FieldRenderer.Render(child, "Description"));
                    GeneralLink leftCTA = new GeneralLink()
                    {
                        Title = new HtmlString(SitecoreAccess.LinkTitle(child.Fields["CTA"])),
                        CTALink = String.IsNullOrEmpty(child.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(child.Fields["CTA"]),
                    };
                    whoWeAreSection.LeftCTA = leftCTA;
                    whoWeAreSection.RightContent = new HtmlString(FieldRenderer.Render(child, "Content"));
                    whoWeAreSection.RightComponent = new HtmlString(FieldRenderer.Render(child, "Component"));
                    whoWeAreSection.Class = String.IsNullOrEmpty(child.Fields["Class"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Class");
                    
                    Sitecore.Data.Items.Item componentDataSource = Sitecore.Context.Database.GetItem(child["Component Datasource"]); ;
                    if (componentDataSource != null) {
                        
                        whoWeAreSection.RightComponent = new HtmlString(FieldRenderer.Render(child, "Component"));
                        


                        switch (whoWeAreSection.RightComponent.ToString())
                        {
                            case "Team Members":
                                TeamMembersService teamMembersService = new TeamMembersService();

                                whoWeAreSection.RightTeamMembers = teamMembersService.GetTeamMembersData(componentDataSource);
                                break;
                            case "Timeline":
                                TimelineService timelineService = new TimelineService();
                                Models.Components.Timeline timeline = new Models.Components.Timeline();
                                timeline = timelineService.GetTimelineData(componentDataSource);
                                whoWeAreSection.RightComponentClass = String.IsNullOrEmpty(child.Fields["Component Class"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Component Class");
                                timeline.Class = whoWeAreSection.RightComponentClass;
                                whoWeAreSection.RightTimeline = timeline;
                                break;
                            default: break;
                        }
                        
                    }

                    if (!isMobile || (whoWeAreSection.Class != null ? !whoWeAreSection.Class.ToLower().Contains("hide-mobile") : true))
                        whoWeAre.SectionCollection.Add(whoWeAreSection);
                }
            }

            
            return whoWeAre;
        }

    }
}
