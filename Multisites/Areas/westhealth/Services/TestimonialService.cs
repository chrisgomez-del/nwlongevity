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
            Item datasource = GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Image));
                model.Testimonial = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Testimonial));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Testimonial.Fields.Copy));

            }
            return model;
        }
        public Item GetDataSourceItem()
        {
            var dataSourceId = RenderingContext.CurrentOrNull.Rendering.DataSource;
            return GetItemById(dataSourceId);
        }

        public Item GetItemById(ID id)
        {
            return Sitecore.Context.Database.GetItem(id);
        }
        public Item GetItemById(string path)
        {
            return Sitecore.Context.Database.GetItem(path);
        }
    }
}