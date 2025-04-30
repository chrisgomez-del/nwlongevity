using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Helper
{
    public static class RenderingParameters
    {
        public static string GetValueFromCurrentRenderingParameters(string parameterName)
        {
            var rc = RenderingContext.CurrentOrNull;
            if (rc == null || rc.Rendering == null) return string.Empty;
            var parametersAsString = rc.Rendering.Properties["Parameters"];
            var parameters = HttpUtility.ParseQueryString(parametersAsString);
            return parameters[parameterName] ?? string.Empty;
        }

        public static Item[] GetItemsFromCurrentRenderingParameters(string parameterName)
        {
            List<Item> result = new List<Item>();
            var rc = RenderingContext.CurrentOrNull;
            if (rc != null)
            {
                var itemIds = GetValueFromCurrentRenderingParameters(parameterName)
                    ?? string.Empty;
                var db = rc.ContextItem.Database;

                var selectedItemIds = itemIds.Split('|');
                foreach (var itemId in selectedItemIds)
                {
                    Guid id = Guid.Empty;
                    if (Guid.TryParse(itemId, out id))
                    {
                        var found = db.GetItem(new ID(id));
                        if (found != null)
                        {
                            result.Add(found);
                        }
                    }
                }
            }
            return result.ToArray();
        }
    }
}