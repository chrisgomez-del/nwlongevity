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
    public interface IHeadlineCopyBlockService
    {
        HeadlineCopyBlock GetData();
    }
    public class HeadlineCopyBlockService : IHeadlineCopyBlockService
    {
        public HeadlineCopyBlock GetData()
        {
            HeadlineCopyBlock data = new HeadlineCopyBlock();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.Heading = new HtmlString(FieldRenderer.Render(datasource, "Heading"));
                data.BodyTitle = new HtmlString(FieldRenderer.Render(datasource, "BodyTitle"));
                data.BodyContent = new HtmlString(FieldRenderer.Render(datasource, "BodyContent"));

            }
            return data;
        }
    }
}