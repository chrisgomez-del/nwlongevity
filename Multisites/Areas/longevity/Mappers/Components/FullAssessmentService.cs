using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IFullAssessmentService
    {
        FullAssessment GetAssessmentData();

    }
    public class FullAssessmentService : IFullAssessmentService
    {
        public FullAssessment GetAssessmentData() 
        {
            FullAssessment fullAssessment = new FullAssessment();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                fullAssessment.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));

                foreach (Sitecore.Data.Items.Item child in datasource.Children)
                {
                    Assessment assessment = new Assessment();
                    assessment.VideoPath = String.IsNullOrEmpty(child.Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Video");
                    assessment.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                    assessment.Content = new HtmlString(FieldRenderer.Render(child, "Content"));
                    fullAssessment.AssessmentCollection.Add(assessment);
                }
            }

            return fullAssessment;
        }

    }
}
