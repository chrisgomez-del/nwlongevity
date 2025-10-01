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
                                whoWeAreSection.RightTeamMembers = GetTeamMembers(componentDataSource);
                                break;
                            case "Timeline":
                                whoWeAreSection.RightTimelineEvents = GetTimelineEvents(componentDataSource);
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

        public List<TeamMember> GetTeamMembers(Sitecore.Data.Items.Item componentDataSource) {
            
            List<TeamMember> teamMembers = new List<TeamMember>();
            
            TeamMemberRow memberrow = new TeamMemberRow();

            ChildList children = componentDataSource.GetChildren();
            foreach (Sitecore.Data.Items.Item row in children)
            {
                ChildList children2 = row.GetChildren();
                foreach (Sitecore.Data.Items.Item child2 in children2)
                {

                    TeamMember teamMember = new TeamMember();
                    teamMember.ProfileImagePath = String.IsNullOrEmpty(child2.Fields["Profile Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child2, "Profile Image");
                    teamMember.Title = new HtmlString(FieldRenderer.Render(child2, "Title"));
                    GeneralLink name = new GeneralLink()
                    {
                        Title = new HtmlString(SitecoreAccess.LinkTitle(child2.Fields["Name"])),
                        CTALink = String.IsNullOrEmpty(child2.Fields["Name"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(child2.Fields["Name"]),
                    };
                    teamMember.Name = name;
                    teamMembers.Add(teamMember);
                }

            }

            return teamMembers;
        }

        public List<TimelineEvent> GetTimelineEvents(Sitecore.Data.Items.Item componentDataSource)
        {

            List<TimelineEvent> timelineEvents = new List<TimelineEvent>();

            ChildList children = componentDataSource.GetChildren();
            foreach (Sitecore.Data.Items.Item child in children)
            {
                TimelineEvent timelineEvent = new TimelineEvent();

                timelineEvent.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                timelineEvent.Content = new HtmlString(FieldRenderer.Render(child, "Content"));
                timelineEvent.NavTitle = new HtmlString(FieldRenderer.Render(child, "Nav Title"));
                timelineEvent.VideoPath = String.IsNullOrEmpty(child.Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Video");
                timelineEvent.VideoAriaLabel = String.IsNullOrEmpty(child.Fields["Video Aria Label"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Video Aria Label");
                timelineEvent.ImagePath = String.IsNullOrEmpty(child.Fields["Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Image");
                timelineEvent.ImageAlt = String.IsNullOrEmpty(child.Fields["Image Alt"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Image Alt");
                timelineEvent.InfoBoxTop = String.IsNullOrEmpty(child.Fields["Info Box Top"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Info Box Top");
                timelineEvent.InfoBoxLeft = String.IsNullOrEmpty(child.Fields["Info Box Left"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Info Box Left");
                timelineEvents.Add(timelineEvent);
            }

            return timelineEvents;
        }
    }
}
