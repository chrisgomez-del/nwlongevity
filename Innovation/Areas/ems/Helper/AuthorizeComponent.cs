using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Mvc.Presentation;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace EMS.Areas.ems.Helper
{
    public class AuthorizeComponent : FilterAttribute
    {
         //filterContext.Controller.ViewBag.AutherizationMessage = "Custom Authorization: Message from OnAuthorization method.";
            public void OnAuthorization(HttpActionContext actionContext)
        {
            var user = LoginHelper.GetUser("sitecore", "admin", "b");
            var isauth = LoginHelper.Login(user);
            if ((Context.User.IsAdministrator || Context.User.IsInRole("sitecore\\analytics reporting")) && (Context.User.IsAuthenticated))
                return;
            string message = Translate.Text("Unauthorized Access");
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
        }

    }
    public class RequiresAuthorization : FilterAttribute, IActionFilter
    {
        
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = LoginHelper.GetUser("sitecore", "admin", "b");
            var isauth = LoginHelper.Login(user);
            if(!isauth)
            {
                filterContext.Result = new EmptyResult();

            }
            
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Not required.
        }
    }
    public class RequiresDataSource : FilterAttribute, IActionFilter
    {
        private const string DefaultNoDataSourceView = "_NoDataSource";
        protected Item DataSourceItem { get; set; }
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DataSourceItem = GetDataSourceItem();
            if (DataSourceItem == null)
            {
                filterContext.Result = new PartialViewResult
                {
                    ViewName = NoDataSourceView ?? DefaultNoDataSourceView
                };
            }
        }
        private Item GetDataSourceItem()
        {
            if (RenderingContext.Current == null || RenderingContext.Current.Rendering == null ||
                string.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
            {
                return null;
            }
            // A DataSource has at least been set. Now to find out if it is an actual item.
            return Sitecore.Context.Database.GetItem(RenderingContext.Current.Rendering.DataSource);
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Not required.
        }
        public string NoDataSourceView { get; set; }
    }
}