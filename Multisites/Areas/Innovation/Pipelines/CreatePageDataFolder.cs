using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetRenderingDatasource;
using Sitecore.SecurityModel;
using Sitecore.Text;

namespace Innovation.Areas.Innovation.Pipelines
{
    public class CreatePageDataFolder
    {
        private static readonly ID DataSourceLocationField = new ID("{B5B27AF1-25EF-405C-87CE-369B3A004016}");
        private static readonly ID FolderTemplateId = new ID("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}");
        private static readonly TemplateID FolderTemplate = new TemplateID(FolderTemplateId);
        private const string RelativePath = "./";

        public void Process(GetRenderingDatasourceArgs args)
        {
            Assert.IsNotNull((object)args, nameof(args));
            foreach (string str in new ListString(args.RenderingItem["Datasource Location"]))
            {
                bool hasQuery = false;
                bool isRelativePath = false;
                string dataSourceLocation = str;
                string contextItemPath = args.ContextItemPath;
                string contextItemId = args.ContentDatabase.GetItem(contextItemPath).ID.ToString();

                if (str.StartsWith("query:", StringComparison.InvariantCulture))
                    hasQuery = true;
                if (str.Contains("./"))
                    isRelativePath = true;
                if (!string.IsNullOrEmpty(contextItemPath))
                {
                    Item currentItem = args.ContentDatabase.GetItem(contextItemPath);
                    if (currentItem != null)
                    {
                        // Only create folder structure if path is relative
                        if (isRelativePath)
                        {
                            // Split the datasource location (minus the ./ or query:./) into pieces
                            var pathPieces = hasQuery ? str.Substring(8).Split('/') : str.Substring(2).Split('/');

                            // This piece came from comments on the original blog this code was taken from:
                            // WebEditRibbon is subscribed to the ItemCreated notification and reloads the page if any new item is created during the request.
                            // That is why the page is reloaded and all changes (inserted rendering, etc.) are lost when you create the folder for datasources.
                            Client.Site.Notifications.Disabled = true;

                            var parentItem = currentItem;
                            foreach (var pathPiece in pathPieces)
                            {
                                contextItemPath += "/" + pathPiece;

                                // If the item already exists, continue
                                var pathItem = args.ContentDatabase.GetItem(contextItemPath);
                                if (pathItem != null)
                                {
                                    parentItem = pathItem;
                                    continue;
                                }

                                using (new SecurityDisabler())
                                {//creates the page_data folder
                                    parentItem.Add(pathPiece, FolderTemplate);
                                }
                            }

                            Client.Site.Notifications.Disabled = false;
                        }
                    }
                }
            }
        }
    }
}
