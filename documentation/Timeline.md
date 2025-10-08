# Timeline Component

The Timeline component is a Sitecore MVC component that displays a chronological sequence of events, each with a title, content, images, and optional video. The component supports both desktop and mobile views, allowing for rich visual storytelling and navigation through program milestones or historical events.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **Timeline** can have one or more child items called **TimelineEvent**.
- Each **TimelineEvent** child item represents a single event in the timeline and contains the fields listed below.

---

## Fields

### TimelineEvent
- **Title**: HtmlString
- **Content**: HtmlString
- **NavTitle**: HtmlString
- **VideoPath**: string
- **VideoAriaLabel**: string
- **ImagePath**: string
- **ImageAlt**: string
- **InfoBoxTop**: string
- **InfoBoxLeft**: string
- **MobileImagePath**: string

### Timeline
- **Title**: HtmlString
- **Class**: string
- **EventCollection**: List<TimelineEvent>

---

*Add a screenshot of the component above when available.*
