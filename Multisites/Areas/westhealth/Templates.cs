using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Mvc.Diagnostics;
using Sitecore.Publishing;

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
                public static string FooterUtilityNavigationLinks = "Footer Utility Navigation Links";
                public static string Copy = "Copy";

            }
        }
        public static class Global
        {
            public static class Fields
            {
                public static string MetaTitle = "Meta Title";
            }
        }
        public static class Testimonial
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string Testimonial = "Testimonial";
                public static string Copy = "Copy";
                public static string TestimonialAuthor= "Testimonial Author";
            }
        }
        public static class EnhancedCallout
        {
            public static class Fields
            {
                public static string Title = "Title";
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
        public static class AccordionItem
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Image = "Image";
                public static string Copy = "Copy";
            }
        }
        public static class AccordionPanel
        {
            public static class Fields
            {
                public static string Title = "Title";
            }
        }
        public static class NavigationLink
        {
            public static class Fields
            {
            }
        }
        public static class Card
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string SubTitle = "Sub Title";
                public static string Copy = "Copy";
                public static string CtaText = "Cta Text";
                public static string CtaSource = "Cta Source";
            }
        }
        public static class TeamContainer
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Logo = "Logo";
                public static string SubTitle = "Sub-Title";
                public static string Copy = "Copy";
            }
        }
        public static class TeamMember
        {
            public static class Fields
            {
                public static string Name = "Name";
                public static string Qualifications = "Qualifications";
                public static string Titles = "Titles";
                public static string Image = "Image";
                public static string ProfileLink = "Profile Link";
                public static string Copy = "Copy";
                public static string CtaText = "Cta Text";
                public static string CtaSource = "Cta Source";
            }
        }
        public static class TwoColumnWithImage
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string ImageLocation = "Image Location";
            }
        }
        public static class Slider
        {

        }
        public static class Slide
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Copy = "Copy";
                public static string CTA = "CTA";
            }
        }
        public static class TwoColumnImageStack
        {
            public static class Fields
            {
                public static string TopImage = "Top Image";
                public static string TopBackground = "Top Background"; 
                public static string BottomImage = "Bottom Image";
                public static string BottomBackground = "Bottom Background";
            }
        }
    }
}