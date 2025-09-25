using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;
using NM_MultiSites.Areas.Longevity.Models;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface ITransitionCopyService
    {
        TransitionCopy GetTransitionCopyData();

    }
    public class TransitionCopyService : ITransitionCopyService
    {
        public TransitionCopy GetTransitionCopyData()
        {
            TransitionCopy transitionCopy = new TransitionCopy();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                transitionCopy.TransitionContent1 = new HtmlString(FieldRenderer.Render(datasource, "Transition Content 1"));
                transitionCopy.TransitionContent2 = new HtmlString(FieldRenderer.Render(datasource, "Transition Content 2"));

                GeneralLink cta = new GeneralLink()
                {
                    Title = new HtmlString(SitecoreAccess.LinkTitle(datasource.Fields["CTA"])),
                    CTALink = String.IsNullOrEmpty(datasource.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(datasource.Fields["CTA"]),
                };

                transitionCopy.CTA = cta;
            }
            return transitionCopy;
        }

    }
}
