using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;


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
            }
            return transitionCopy;
        }

    }
}
