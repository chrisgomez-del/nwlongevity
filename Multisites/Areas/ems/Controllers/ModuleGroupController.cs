using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.ems.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.ems.Controllers
{
    public class ModuleGroupController : Controller
    {
        
        public ActionResult Index()
        {
            var currentitem = PageContext.Current.Item;
            Item i = SitecoreAccess.getDroplinkSelectedItem(currentitem, "Banner");
            return View("Carousel", GetModules(i));
        }

        public ActionResult Banner()
        {
            Module moduleItem = GetModule("Banner");
            if (moduleItem == null)
            {
                string ImageURL = SitecoreAccess.GetMediaUrl(PageContext.Current.Item, "BannerImage");
                if(!String.IsNullOrWhiteSpace(ImageURL))
                {
                    moduleItem = new Module()
                    {
                        Image = SitecoreAccess.GetMediaUrl(PageContext.Current.Item, "BannerImage"),
                    };
                }
                
            }
            
            return View("Banner", moduleItem);
        }

        public ActionResult Accordion()
        {
            return View("Accordion", GetModules());
        }

        public ActionResult Feature()
        {
            return View("Feature", GetModules());
        }

        public ActionResult Cards()
        {
            return View("Cards", GetModules());
        }

        public ActionResult FolderView()
        {
            return View(GetFolders());
        }

        public ActionResult CardBlock()
        {
            var datasourceId = RenderingContext.Current.Rendering.DataSource;
            var datasource = ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
            Block source = new Block();
            return View(source);
        }

        public ActionResult FeatureBlock()
        {
            var datasourceId = RenderingContext.Current.Rendering.DataSource;
            var datasource = ID.IsID(datasourceId) ? Context.Database.GetItem(datasourceId) : null;
            Block source = new Block();
            return View(source);
        }


        private Module GetModule(string field)
        {
            try
            {
                var currentitem = PageContext.Current.Item;
                Item i = SitecoreAccess.getDroplinkSelectedItem(currentitem,field);
                Module moduleItem = new Module()
                {
                    Item = i,
                    ItemId = i.ID.ToString(),
                    Title = new HtmlString(FieldRenderer.Render(i, "Title")),
                    ShortText = new HtmlString(FieldRenderer.Render(i, "Short Text")),
                    MainContent = new HtmlString(FieldRenderer.Render(i, "MainContent")),
                    link = new HtmlString(FieldRenderer.Render(i, "link")),
                    CTATitle = String.IsNullOrEmpty(i.Fields["CTA Title"].GetValue(true)) ? null : new HtmlString(FieldRenderer.Render(i, "CTA Title")),
                    CTALink = String.IsNullOrEmpty(i.Fields["CTA Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(i.Fields["CTA Link"]),
                    Image = SitecoreAccess.GetMediaUrl(i),
                    ImageFullPath = i.Paths.FullPath,
                    ImagePosition = SitecoreAccess.getDroplinkSelectedItem(i, "ImagePosition") != null ? SitecoreAccess.getDroplinkSelectedItem(i, "ImagePosition").Fields["Value"].Value.Equals("left") : false
                };

                return moduleItem;
            }
            catch (Exception ex)
            {
                Log.Error("Exception :" + ex, this);
            }
            return null;

        }

        private ModuleGroup GetModules(Item mItem= null)
        {


            var ModuleGroup = new ModuleGroup();
            try
            {
                var dataId = RenderingContext.CurrentOrNull.Rendering.DataSource;
                var item = mItem ?? Sitecore.Context.Database.GetItem(dataId);

                List<Item> rows = SitecoreAccess.GetMultiListItems(item,"Modules");
                IEnumerable<Module> rowitem = rows
                    .Select(i => new Module()
                    {
                        Item = i,
                        ItemId = i.ID.ToString(),
                        Title = new HtmlString(FieldRenderer.Render(i, "Title")),
                        ShortText = new HtmlString(FieldRenderer.Render(i, "Short Text")),
                        MainContent = new HtmlString(FieldRenderer.Render(i, "MainContent")),
                        link = new HtmlString(FieldRenderer.Render(i, "link")),
                        CTATitle = String.IsNullOrEmpty(i.Fields["CTA Title"].GetValue(true)) ? null : new HtmlString(FieldRenderer.Render(i, "CTA Title")),
                        CTALink = String.IsNullOrEmpty(i.Fields["CTA Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(i.Fields["CTA Link"]),
                        Image = SitecoreAccess.GetMediaUrl(i),
                        ImageFullPath = i.Paths.FullPath,
                        ImagePosition = SitecoreAccess.getDroplinkSelectedItem(i, "ImagePosition") != null ? SitecoreAccess.getDroplinkSelectedItem(i, "ImagePosition").Fields["Value"].Value.Equals("left") : false
                    }); 
                int colSplit = 4;
                int colStyle = 3;
                try
                {
                    Item colSplitItem = SitecoreAccess.getDroplinkSelectedItem(item, "ColumnStyle");
                    if (colSplitItem != null)
                    {
                        colStyle = Int32.Parse(colSplitItem.Fields["Value"].Value);
                        colSplit = 12 / colStyle;
                    }


                    ModuleGroup.GroupId = dataId.Replace("{", "").Replace("}", "").Replace("-", "");
                    ModuleGroup.GroupTitle = new HtmlString(FieldRenderer.Render(item, "GroupTitle"));
                    ModuleGroup.ColumnStyle = colSplit;
                    ModuleGroup.modules = rowitem;
                    return ModuleGroup;
                }
                catch (Exception ex)
                {
                    Log.Error("Exception :" + ex, this);
                }

                return null;

            }
            catch (Exception ex)
            {
                Log.Error("Exception :" + ex, this);
            }

            return null;

        }

        private FolderGroup GetFolders()
        {
            Item DataSource = RenderingContext.Current.Rendering.Item;
            if (DataSource!=null)
            {
                FolderGroup node = new FolderGroup();
                node.Name = DataSource.Fields["Name"].Value;
                node.parentId = DataSource.ID.ToShortID().ToString();
                node.hasSubitems = true;
                node.Item = DataSource;
                node.Type = "F";
                node.Level = 0;
                node.Order = 0;
                node = recursiveGetFolders(node,node, node.Level, node.Order);
                return node;
            }
            return null;
        }

        private FolderGroup recursiveGetFolders(FolderGroup root,FolderGroup node, int l, int o)
        {
            int order = o;
            int level = l;
            level++;
            order = 0;
            List<Item> mlf = SitecoreAccess.GetMultiListItems(node.Item, "GroupList");
            if (mlf != null)
            {
                foreach (Item i in mlf)
                {
                    FolderGroup fg1 = new FolderGroup();
                    if (i.Paths.IsMediaItem)
                    {
                        fg1.Name = i.Name;
                        fg1.parentId = i.ParentID.ToShortID().ToString();
                        fg1.hasSubitems = false;
                        fg1.Item = i;
                        fg1.Type = "MI";
                        fg1.Level = level;
                        fg1.Order = order++;
                        root.Subitems.Add(fg1);
                    }
                    else
                    {
                        fg1.Name = i.Fields["Name"].Value;
                        fg1.parentId = i.ParentID.ToShortID().ToString();
                        fg1.hasSubitems = true;
                        fg1.Item = i;
                        fg1.Type = "F";
                        fg1.Level = level;
                        fg1.Order = order++;
                        root.Subitems.Add(fg1);
                        recursiveGetFolders(root,fg1, fg1.Level, fg1.Order);
                        }
                }
                return root;
            }
            return root;
        }
    }
}