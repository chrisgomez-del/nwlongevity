using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Pages;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface IArticleService
    {
        ArticleViewModel GetArticleViewModel();

    }
    public class ArticleService : IArticleService
    {
        public ArticleService() { }

        public ArticleViewModel GetArticleViewModel()
        {
            {
                var model = new ArticleViewModel();
                Item datasource = WestHealthSitecoreService.GetDataSourceItem();
                if (datasource != null)
                {
                    model.SourceItem = datasource;
                    model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Title));
                    model.Subtitle = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Subtitle));
                    model.Author = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Author));
                    model.Source = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Source));
                    model.Date = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Date));
                    model.Image = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Image));
                    model.ImageUrl = WestHealthSitecoreService.GetMediaUrl(datasource);
                    model.ImageAltText = WestHealthSitecoreService.GetMediaAltText(datasource);
                    model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.Article.Fields.Copy));
                }

                return model;
            }
        }
    }
}