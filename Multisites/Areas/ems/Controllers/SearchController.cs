using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using EMS.ems.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using EMS.Areas.ems.Helper;
using EMS.Areas.ems.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Resources.Media;
using System.Configuration;

namespace EMS.Controllers
{
    public class SearchController : Controller
    {
       
        public ActionResult Index(String searchterm)
        {
            if (String.IsNullOrWhiteSpace(searchterm))
            {
                return View(GetSearchResults(""));
            }
            else
            {
                return View(GetSearchResults(searchterm));
            }
            
            //return View(GetSearchResults());
        }

       
        private List<Item> getSearchItems(IProviderSearchContext context, String searchterm)
        {
            
            var contextItem = RenderingContext.Current.ContextItem;
            // var nmpnContentPath = ConfigurationManager.AppSettings["nmpn_ContentPathId"]
            // var nmpnMediaPath = ConfigurationManager.AppSettings["nmpn_MediaPathId"];
            var nmpnContentPath = "{30E10EFC-6698-499E-935C-B9BC9B19E583}";
            var nmpnMediaPath = "{893E7B0E-E103-4EB8-8915-F25DBF50A8CF}";
            var nmpnConfigPath = "{3C30229F-DFA3-4A6D-A8B7-18C84A9E33D5}";
            var nmpnModulesoot = "{B33651BA-CFEC-44F9-8DA1-C1BD69DCEE60}";
            var nmpnEventPath = "{115893AC-6928-4618-8D74-66CB71938A73}";
            var nmpnSettingPath = "{A512C46F-1A51-43BC-BB6A-97061FEA9B45}";
            var contentPathId = new Sitecore.Data.ID(nmpnContentPath);
            var mediaPathId = new Sitecore.Data.ID(nmpnMediaPath);
            var configurationPathId = new Sitecore.Data.ID(nmpnConfigPath);
            var ModulesPathId = new Sitecore.Data.ID(nmpnModulesoot);
            var EventsPathId = new Sitecore.Data.ID(nmpnEventPath);
            var settingsPathId = new Sitecore.Data.ID(nmpnSettingPath);
            List<Item> results = null;
            if(String.Equals(searchterm, "event", StringComparison.OrdinalIgnoreCase)
                || String.Equals(searchterm, "events", StringComparison.OrdinalIgnoreCase)){
                results = context.GetQueryable<SearchResultItem>()
                    .Where(x => (x.Content.Contains(searchterm)
                            && x.Paths.Contains(contentPathId)
                            && !x.Paths.Contains(configurationPathId)
                           && !x.Paths.Contains(ModulesPathId)
                           && !x.Paths.Contains(settingsPathId)
                           )
                           || x.Paths.Contains(EventsPathId)                           
                           )
                   .Select(i => (Item)i.GetItem()).ToList();
            }
            else
            {
                results = context.GetQueryable<SearchResultItem>()
                    .Where(x => x.Content.Contains(searchterm)
                           && ((x.Paths.Contains(contentPathId)
                           || x.Paths.Contains(mediaPathId))
                           && !x.Paths.Contains(configurationPathId)
                           && !x.Paths.Contains(ModulesPathId)
                            && !x.Paths.Contains(settingsPathId)
                           )
                           )
                   .Select(i => (Item)i.GetItem()).ToList();
            }
            
            return results;

        }

        private SearchResult buildSearchResult(IProviderSearchContext context, String searchterm)
        {
            var model = new SearchResult()
            {
                SearchTerm = searchterm,
                DataAvailable =false
            };
            HashSet<string> urlSet = new HashSet<string>();
            var results = getSearchItems(context, searchterm);
            foreach (var item in results)
            {
                String title = string.Empty;
                String url = string.Empty;
                string description = string.Empty;
                var includeInSearch = true;
                try
                {
                    if (item.Fields["Exclude from search"].GetValue(true) == "1")
                    {
                        includeInSearch = false;
                    }
                }
                catch (Exception e)
                {
                    //fail silently
                }
                if (item.Paths.IsMediaItem)
                {
                    url =  MediaManager.GetMediaUrl(item) ;
                    title = !String.IsNullOrEmpty(item["Title"]) ? item["Title"] : item.Name;
                    description = item["Description"];
                }
                else
                {
                    url =  LinkManager.GetItemUrl(item);
                    title = !String.IsNullOrEmpty(item["SearchTitle"]) ? item["SearchTitle"] : item["Meta Title"];
                    if (String.IsNullOrWhiteSpace(title))
                    {
                        title = item.Name;
                    }
                    description = item["SearchDescription"];                  

                }
               // url = item.Paths.IsMediaItem ? MediaManager.GetMediaUrl(item) : LinkManager.GetItemUrl(item);
               // title = !String.IsNullOrEmpty(item["Title"]) ? item["Title"] : item["Meta Title"];
                //get only pdf, doc and docx from media. skip png, jpg ..
                if ((String.IsNullOrEmpty(item["Extension"])
                    || item["Extension"].Equals("pdf", StringComparison.InvariantCultureIgnoreCase)
                    || item["Extension"].Equals("doc", StringComparison.InvariantCultureIgnoreCase)
                    || item["Extension"].Equals("docx", StringComparison.InvariantCultureIgnoreCase))
                    && !urlSet.Contains(url) && includeInSearch
                    && !String.IsNullOrEmpty(title))
                {
                    urlSet.Add(url);
                    model.DataAvailable = true;
                    model.Add(
                        new SearchItem()
                        {
                            Title = title,
                            Description = description,
                            Url = url,
                            Type = item["Extension"]
                        });
                }
            }
            return model;

        }
    

        private SearchResult GetSearchResults(String searchterm)
        {
            var index = ContentSearchManager.GetIndex("sitecore_web_index");
            var model = new SearchResult();
            using (var context = index.CreateSearchContext())
            {
              model=  buildSearchResult(context, searchterm);
            }                  
            return (model);
        }        
    }
}