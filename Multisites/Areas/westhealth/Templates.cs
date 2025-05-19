using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace NM_MultiSites.Areas.westhealth
{
    public static class Templates
    {
        public static class Placeholders
        {
            public static string Main = "westhealth-main";
        }
        public static class Config
        {
            public static ID ItemID = new ID("{013D630A-8F72-42DE-B47B-3DEF7135CBDE}");
            public static class Fields
            {
                public static string HeaderNavigationLinks = "Header Navigation Links";
                public static string HeaderLogo = "Header Logo";
                public static string FooterNavigationLinks = "Footer Navigation Links";
                public static string SocialNavigationLinks = "Social Links";
                public static string FooterLogo = "Footer Logo";
                public static string FooterUtilityNavigationLinks= "Footer Utility Navigation Links";
                public static string Copy = "Copy";

            }
        }
        public static class Testimonial
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string Testimonial = "Testimonial";
                public static string Copy = "Copy";
            }
        }
        public static class GenericLink
        {
            public static string TemplateID = "{2A2633CF-037B-40AE-9026-EEE2B4835D07}";
            public static string TemplateName = "Generic Link";
            public static class Fields
            {
                public static string LinkSource = "Link Source";
                public static string LinkText = "Link Text";
                public static string Icon = "Icon";
            }
        }
        public static class NavigationLink
        {
            public static class Fields
            {
            }
        }
    }
}