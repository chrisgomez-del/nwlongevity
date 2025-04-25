using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovation.Areas.Innovation.Controllers
{
    public class JsonResponseController : Controller
    {
        // GET: Innovation/JsonResponse
        public ContentResult Index()
        {
            // Retrieve JSON to render from header
            var serializedJson = Request.Headers["SerializedJson"] ?? string.Empty;

            Response.ContentType = "application/json";
            return Content(serializedJson);
        }
    }
}