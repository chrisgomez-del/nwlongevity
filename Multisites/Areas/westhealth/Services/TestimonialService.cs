using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ITestimonialService
    {
        TestimonialViewModel GetTestimonialViewModel();

    }
    public class TestimonialService : ITestimonialService
    {
        public TestimonialViewModel GetTestimonialViewModel()
        {
            var model = new TestimonialViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Image));
                model.Testimonial = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Testimonial));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Copy));
                model.TestimonialAuthor = string.IsNullOrEmpty(datasource.Fields[Templates.Testimonial.Fields.TestimonialAuthor].GetValue(true)) ?
                    null:
                    new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.TestimonialAuthor));


            }
            return model;
        }
    }
}