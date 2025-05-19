using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.westhealth.Models;
using NM_MultiSites.Areas.westhealth.Models.Navigation;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface INavigationService
    {
        FooterViewModel GetFooter();
        HeaderViewModel GetHeader();
    }
    public class NavigationService : INavigationService
    {
        public NavigationService() { }
        public FooterViewModel GetFooter()
        {
            FooterViewModel footer = new FooterViewModel();
            Item i = WestHealthSitecoreService.GetSiteSettingItem();
            List<Item> footerNavItems = WestHealthSitecoreService.GetMultiListItems(i, Templates.Config.Fields.FooterNavigationLinks);
            if (footerNavItems != null && footerNavItems.Any())
            {
                foreach (Item item in footerNavItems)
                {
                    var link = MapGenericLinkViewModel(item);
                    footer.Links.Add(link);
                }
            }

            List<Item> socialNavItems = WestHealthSitecoreService.GetMultiListItems(i, Templates.Config.Fields.SocialNavigationLinks);
            if (socialNavItems != null && socialNavItems.Any())
            {
                foreach (Item item in socialNavItems)
                {
                    var link = MapGenericLinkViewModel(item);
                    footer.SocialLinks.Add(link);
                }
            }
            return footer;
        }
        public HeaderViewModel GetHeader()
        {
            HeaderViewModel header = new HeaderViewModel();
            Item i = WestHealthSitecoreService.GetSiteSettingItem();
            List<Item> headerNavItems = WestHealthSitecoreService.GetMultiListItems(i, Templates.Config.Fields.HeaderNavigationLinks);
            if (headerNavItems != null && headerNavItems.Any())
            {
                foreach (Item item in headerNavItems)
                {
                    header.Links.Add(MapNavigationLinkViewModel(item));
                }
            }
            header.Logo = WestHealthSitecoreService.GetMediaUrl(i, Templates.Config.Fields.HeaderLogo);
            return header;

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