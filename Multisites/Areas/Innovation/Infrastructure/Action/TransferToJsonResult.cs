using System.Collections.Specialized;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.Innovation.Infrastructure.Action
{
    public class TransferToJsonResult : ActionResult
    {
        public TransferToJsonResult(string serializedJson)
        {
            SerializedJson = serializedJson ?? string.Empty;
        }

        public string SerializedJson { get; }

        public override void ExecuteResult(ControllerContext context)
        {
            // Create a header to hold the serialized JSON value
            var headers = new NameValueCollection
            {
                ["SerializedJson"] = SerializedJson
            };

            // And pass in the transfer request so it's available to the controller
            // that picks up generating the response.
            context.HttpContext.Server.TransferRequest("/JsonResponse", false, null, headers);
        }
    }
}