using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models;
using NM_MultiSites.Areas.westhealth.Models.Navigation;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface INavigationService
    {
        FooterViewModel GetFooter();
        HeaderViewModel GetHeader();
        InternalNavigationViewModel GetInternalNavigation();
    }
    public class NavigationService : INavigationService
    {
        public NavigationService() { }


        public FooterViewModel GetFooter()
        {
            FooterViewModel footer = new FooterViewModel();
            Item i = WestHealthSitecoreService.GetSiteSettingItem();

            footer.Links.AddRange(MapMultiListItems<GenericLinkViewModel>(i, Templates.Config.Fields.FooterNavigationLinks, MapGenericLinkViewModel));

            footer.SocialLinks.AddRange(MapMultiListItems<GenericLinkViewModel>(i, Templates.Config.Fields.SocialNavigationLinks, MapGenericLinkViewModel));

            footer.FooterUtilityLinks.AddRange(MapMultiListItems<GenericLinkViewModel>(i, Templates.Config.Fields.FooterUtilityNavigationLinks, MapGenericLinkViewModel));

            footer.Copy = new HtmlString(FieldRenderer.Render(i, Templates.Config.Fields.Copy));

            return footer;
        }
        public HeaderViewModel GetHeader()
        {
            HeaderViewModel header = new HeaderViewModel();
            Item i = WestHealthSitecoreService.GetSiteSettingItem();
            header.Links.AddRange(MapMultiListItems<NavigationLinkViewModel>(i, Templates.Config.Fields.HeaderNavigationLinks, MapNavigationLinkViewModel));

            header.Logo = WestHealthSitecoreService.GetMediaUrl(i, Templates.Config.Fields.HeaderLogo);
            return header;

        }
        public InternalNavigationViewModel GetInternalNavigation()
        {
            InternalNavigationViewModel model = new InternalNavigationViewModel();
            var items = GetCurrentPageDataDirectoryItemsOfType(Templates.NavigableSectionBase.TemplateId);

            foreach(var item in items)
            {
                model.Links.Add(MapInternalNavigationViewModel(item));
            }


            return model;

        }
        public static List<Item> GetCurrentPageDataDirectoryItemsOfType(string type)
        {
            var root = RenderingContext.Current.ContextItem;
            var pageData = root.Axes.GetDescendants()
                .FirstOrDefault(x => x.Name == "page_data");

            return pageData.Axes.GetDescendants()
                .Where(x => x.InheritsFrom(Templates.NavigableSectionBase.TemplateId))                //&& x.Fields[Templates.NavigableSectionBase.Fields.IncludeInNavigation].Value(true) == true)''
                .ToList();

        }
        private InternalNavigationLinkViewModel MapInternalNavigationViewModel(Item currentInternalNavigationItem)
        {
            return new InternalNavigationLinkViewModel
            {
                SectionId = currentInternalNavigationItem.Fields[Templates.NavigableSectionBase.Fields.SectionId].GetValue(true),
                Title = new HtmlString(FieldRenderer.Render(currentInternalNavigationItem, Templates.NavigableSectionBase.Fields.Title))
            };
        }
        private List<T> MapMultiListItems<T>(Item item, string fieldName, Func<Item, T> MappingFunction)
            where T : class
        {
            List<Item> items = WestHealthSitecoreService.GetMultiListItems(item, fieldName);
            var result = new List<T>();

            if (items != null && items.Any())
            {
                foreach (var i in items)
                {
                    result.Add(MappingFunction(i));
                }
            }
            return result;
        }
        private List<NavigationLinkViewModel> GetChildNavigationItems(Item currentNavItem)
        {
            var children = currentNavItem.Children.Where(x => x.InheritsFrom(Templates.GenericLink.TemplateID));
            if (children.Any())
            {
                return children.Select(x => MapNavigationLinkViewModel(x)).ToList();
            }
            return null;

        }
        private NavigationLinkViewModel MapNavigationLinkViewModel(Item item)
        {
            return new NavigationLinkViewModel
            {
                LinkText = new HtmlString(FieldRenderer.Render(item, Templates.GenericLink.Fields.LinkText)),
                LinkSource = string.IsNullOrEmpty(item.Fields[Templates.GenericLink.Fields.LinkSource].GetValue(true)) ?
                            null :
                            WestHealthSitecoreService.LinkUrl(item.Fields[Templates.GenericLink.Fields.LinkSource]),
                Children = GetChildNavigationItems(item)
            };
        }
        private GenericLinkViewModel MapGenericLinkViewModel(Item item)
        {
            return new GenericLinkViewModel
            {
                LinkText = new HtmlString(FieldRenderer.Render(item, Templates.GenericLink.Fields.LinkText)),
                LinkSource = String.IsNullOrEmpty(item.Fields[Templates.GenericLink.Fields.LinkSource].GetValue(true)) ?
                    null :
                    WestHealthSitecoreService.LinkUrl(item.Fields[Templates.GenericLink.Fields.LinkSource]),
                Icon = WestHealthSitecoreService.GetMediaUrl(item, Templates.GenericLink.Fields.Icon)
            };
        }

    }
}