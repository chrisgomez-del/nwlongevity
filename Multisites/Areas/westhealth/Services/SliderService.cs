using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data.Items;
using Sitecore.Syndication;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ISliderService
    {
        SlideViewModel GetSlideViewModel();
        SliderViewModel GetSliderViewModel();
    }
    public class SliderService : ISliderService
    {
        public SlideViewModel GetSlideViewModel()
        {
            var model = new SlideViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();

            if (datasource != null)
            {
                model.SourceItem = datasource;

                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.Slide.Fields.Title));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Slide.Fields.Copy));
                model.CTA = new HtmlString(FieldRenderer.Render(datasource, Templates.Slide.Fields.CTA));
            }
            return model;
        }
        public SliderViewModel GetSliderViewModel()
        {
            var model = new SliderViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
            }
            return model;
        }
    }
}