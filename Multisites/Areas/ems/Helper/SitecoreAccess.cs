using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Helper
{
    public static class SitecoreAccess
    {
        public static Item getSiteRoot()
        {
            var rootItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath + Sitecore.Context.Site.StartItem);
            return rootItem;
        }
        public static Item getSiteSettingItem()
        {
            var settingsItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath + Sitecore.Context.Site.StartItem + "/config/global");
            return settingsItem;
        }
        public static Item getDroplinkSelectedItem(Item source, string fieldName)
        {
            if (source != null)
            {
                LookupField field = (LookupField)source.Fields[fieldName];
                Item selectedItem = field?.TargetItem;
                return selectedItem;
            }

            return null;
        }

        public static Item getRenderingparameterDroplinkSelectedItem(string fieldName)
        {
            string selectedItemId = RenderingContext.Current.Rendering.Parameters[fieldName];

            if (!String.IsNullOrEmpty(selectedItemId))
            {
                var selectedItem = RenderingContext.Current.ContextItem.Database.GetItem(ID.Parse(Guid.Parse(selectedItemId)));
                //var parameter = selectedItem["Value"];
                return selectedItem;
            }

            return null;
        }

        public static String GetMediaUrl(Item item, string field = "Image")
        {
            var url = String.Empty;
            if (item != null && item.Fields[field] != null)
            {
                Sitecore.Data.Fields.ImageField imgField = ((Sitecore.Data.Fields.ImageField)item.Fields[field]);
                try
                {
                    url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Image not authoed", ex, "EMS");
                    return String.Empty;
                }

            }
            return url;
        }

        public static List<Item> GetMultiListItems(Item dataItem, string fieldName)
        {
            if (dataItem != null && !string.IsNullOrEmpty(fieldName))
            {
                MultilistField field = dataItem.Fields[fieldName];
                if (null == field)
                    return null;

                return field.GetItems().ToList();
            }
            return null;
        }
        public static string GetDateTime(Item item, string field)
        {
            var dateField = (DateField)item.Fields[field];
            var serverTime = Sitecore.DateUtil.ToServerTime(dateField.DateTime);
            string date = Sitecore.DateUtil.FormatShortDateTime(serverTime);
            return date;
        }
        public static bool HasValue(Item item, string field)
        {
            if (item != null && item.Fields[field] != null && !String.IsNullOrWhiteSpace(item.Fields[field].Value))
            {
                return true;

            }
            return false;
        }

        public static String LinkUrl(this Sitecore.Data.Fields.LinkField lf)
        {
            switch (lf.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    return lf.TargetItem != null ? Sitecore.Links.LinkManager.GetItemUrl(lf.TargetItem) : string.Empty;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    return lf.TargetItem != null ? Sitecore.Resources.Media.MediaManager.GetMediaUrl(lf.TargetItem) : string.Empty;
                case "external":
                    // Just return external links
                    return lf.Url;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    return !string.IsNullOrEmpty(lf.Anchor) ? "#" + lf.Anchor : string.Empty;
                case "mailto":
                    // Just return mailto link
                    return lf.Url;
                case "javascript":
                    // Just return javascript
                    return lf.Url;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    return lf.Url;
            }
        }
    }
}