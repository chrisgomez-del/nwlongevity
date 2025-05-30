using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IResearchListService
    {
        ResearchListViewModel GetResearchListViewModel();

    }
    public class ResearchListService : IResearchListService
    {
        public ResearchListService()
        {

        }

        public ResearchListViewModel GetResearchListViewModel()
        {
            {
                var model = new ResearchListViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.TwoColumnStaggeredList.Fields.Title));
                }

                return model;
            }
        }
    }
}