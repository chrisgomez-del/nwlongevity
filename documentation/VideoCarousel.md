# Video Carousel Component

The Video Carousel component is a Sitecore MVC component that displays a collection of videos, optionally supporting separate videos for mobile and desktop devices. Each video can be marked as mobile-specific, and the component can display titles and descriptions alongside the video content. This component is typically used to showcase introductory or promotional videos in a visually engaging way.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **VideoCarousel** can have one or more child items called **Video**.
- Each **Video** child item represents a single video in the component and contains the fields listed below.

---

## Fields

### Video
- **VideoPath**: string
- **IsMobile**: bool

### VideoCarousel
- **VideoCollection**: List<Video>
- **Title1**: HtmlString
- **Title2**: HtmlString
- **Description**: HtmlString
- **Class**: string

---

*Add a screenshot of the component above when available.*
