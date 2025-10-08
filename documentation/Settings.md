# Settings Template (Header & Footer)

The Settings template is a Sitecore template used to store global configuration for the site, including header and footer content. Items based on this template typically reside under a path like `/sitecore/content/[site]/home/config/global` and provide centralized management for navigation, branding, and legal information.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **Settings** is used for global configuration.
- The settings item contains fields for both header and footer content, which are referenced throughout the site.

---

## Header Fields
- **Title**: HtmlString
- **MainNavLinks**: List<GeneralLink>
- **AdditionalNavLinks**: List<GeneralLink>
- **GenericContent**: HtmlString
- **SubTitle**: HtmlString
- **IsHomePageHeader**: bool
- **CaseStudyImage**: HtmlString
- **AdditionalContent**: HtmlString

## Footer Fields
- **Title1**: HtmlString
- **Title2**: HtmlString
- **FormButtonLabel**: HtmlString
- **PoliciesLink**: GeneralLink
- **AccessibilityLink**: GeneralLink
- **Copyright**: HtmlString
- **Disclaimer**: HtmlString

---

*Add a screenshot of the settings item in Sitecore above when available.*
