using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using Google.Apis.Http;
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
            public static class Folders
            {
                public static string PageData = "page_data";
            }
            public static class Fields
            {
                public static string MetaTitle = "Meta Title";
                public static string MetaDescription = "Meta Description";
                public static string MetaKeywords = "Meta Keywords";
            }
        }
        public static class InternalNavigation
        {
            public static class Fields
            {
                public static string NavigationLinks = "Navigation Links";
            }
        }
        public static class NavigableSectionBase
        {
            public static string TemplateId = "{3AD5F43C-69EC-493E-AC87-43D07211517F}";
            public static class Fields
            {
                public static string SectionId = "Section ID";
                public static string SectionTitle = "Section Title";
                public static string IncludeInNavigation = "Include in Navigation";
            }
        }
        public static class Testimonial
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string Testimonial = "Testimonial";
                public static string Copy = "Copy";
                public static string TestimonialAuthor = "Testimonial Author";
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
            }
        }
        public static class SocialLink
        {
            public static class Fields
            {
                public static string LinkSource = "Link Source";
                public static string LinkText = "Link Text";
                public static string LinkIconClass = "Link Icon Class";
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
                public static string Image = "Image";
                public static string SectionId = "Section Id";
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
                public static string Image = "Image";
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
                public static string SectionId = "Section Id";
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
                public static string Image = "Image";
                public static string ImageLocation = "Image Location";
                public static string CssClass = "Css Class";
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
        public static class TwoColumnStaggeredList
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string LeftList = "Left List";
                public static string RightList = "Right List";
                public static string Image = "Image";
                public static string LeftListCtaSource = "Left List Cta Source";
                public static string RightListCtaSource = "Right List Cta Source";
                public static string LeftListTitle = "Left List Title";
                public static string RightListTitle = "Right List Title";
                public static string LeftListTab = "Left List Tab";
                public static string RightListTab = "Right List Tab";
                public static string SectionId = "Section Id";
            }
        }
        public static class ResearchList
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string CardLocation = "Card Location";
                public static string SectionId = "Section Id";
            }
        }
        public static class SplitContentHero
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string ImageLocation = "Image Location";
                public static string BackgroundColor = "Background Color";
                public static string CssClass = "Css Class";
            }
        }
        public static class SplitContentSubtitle
        {
            public static class Fields
            {
                public static string Image = "Image";
                public static string ImageLocation = "Image Location";
                public static string BackgroundColor = "Background Color";
                public static string CssClass = "Css Class";
            }
        }
        public static class ResearchCard
        {
            public static string TemplateID = "{BE49D9AF-40B1-4256-9581-7E1AE7F68D7B}";
            public static class Fields
            {
                public static string Title = "Title";
                public static string Source = "Source";
                public static string CtaSource = "Cta Source";
                public static string BackgroundColor = "Background Color";
                public static string CssClass = "Css Class";
            }
        }
        public static class CardList
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string BackgroundColor = "Background Color";
                public static string CssClass = "Css Class";
                public static string SectionId = "Section Id";
            }
        }
        public static class ThreeCardCallout
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Image = "Image";
                public static string SectionId = "Section Id";
            }
        }
        public static class NavigableTabs
        {
            public static string TemplateID = "{A4BD525A-7773-496B-B839-46BCE6E5CB0D}";
            public static class Fields
            {
                public static string Title = "Title";
                public static string NavigableTabsLocation = "Navigable Tabs Location";
                public static string SectionId = "Section Id";
            }
        }
        public static class NavigableTab
        {
            public static string TemplateID = "{A65CF74A-C1A7-43ED-947F-4FC0BEB73D95}";
            public static class Fields
            {
                public static string Title = "Title";
            }
        }
        public static class TabResource
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Copy = "Copy";
                public static string PdfSource = "Pdf Source";
                public static string PdfText = "Pdf Text";
                public static string CtaSource = "Cta Source";
                public static string CtaText = "Cta Text";
            }
        }
        public static class Article
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Subtitle = "Subtitle";
                public static string Author = "Author";
                public static string Copy = "Copy";
                public static string Source = "Source";
                public static string Image = "Image";
                public static string Date = "Date";

            }
        }
        public static class RingDiagram
        {
            public static string RingTemplateID = "{666D5F1D-1AF7-4456-9903-093AC1E521CE}";
            public static class Fields
            {
                public static string Title = "Title";
                public static string Copy = "Copy";
                public static string RingLocation = "Ring Location";
            }
        }
        public static class Ring
        {
            public static string CardTemplateID = "{9F23EFD2-120C-41ED-B49B-3D103D464BDB}";
            public static class Fields
            {
                public static string Label = "Label";
                public static string ShortLabel = "Short Label";
                public static string ThemeColor = "Theme Color";
                public static string ThemeCssColor = "Css Class";
            }
        }
        public static class ImageAboveTwoColumn
        {
            public static class Fields
            {
                public static string Title = "Title";
                public static string Copy = "Copy";
                public static string CtaText = "Cta Text";
                public static string CtaSource = "Cta Source";
                public static string CardListTitle = "Card List Title";
                public static string Image = "Image";
                public static string SectionId = "Section Id";
            }
        }
    }
}