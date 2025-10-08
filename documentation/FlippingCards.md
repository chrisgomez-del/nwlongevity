# Flipping Cards Component

The Flipping Cards component is a Sitecore MVC component that displays a collection of interactive cards. Each card can show a title, image, descriptive text, and optionally a call-to-action (CTA) link. Cards can be flipped to reveal additional information on the back side. This component is typically used to highlight features, case studies, or other content in a visually engaging way.

---

## Sitecore Template Structure
A template called **FlippingCards** can have one or more child items called **FlippingCard**.
Each **FlippingCard** child item represents a single card in the component and contains the fields listed below.

---

## Fields

### FlippingCard
- **Title**: HtmlString
- **ImagePath**: string
- **TopText**: HtmlString
- **MiddleText**: HtmlString
- **BottomText**: HtmlString
- **CTADescription**: HtmlString
- **CTA**: GeneralLink
- **IsCTACard**: bool

### FlippingCards
- **FlippingCardCollection**: List<FlippingCard>
