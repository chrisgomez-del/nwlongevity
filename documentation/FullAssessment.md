# Full Assessment Component

The Full Assessment component is a Sitecore MVC component that displays a collection of comprehensive self-assessment items. Each assessment can include a title, content, and associated videos for desktop and mobile devices. This component is typically used to present multi-organ or multi-topic assessments in a structured, interactive format.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **FullAssessment** can have one or more child items called **Assessment**.
- Each **Assessment** child item represents a single assessment in the component and contains the fields listed below.

---

## Fields

### Assessment
- **Title**: HtmlString
- **Content**: HtmlString
- **VideoPath**: string
- **MobileVideoPath**: string

### FullAssessment
- **AssessmentCollection**: List<Assessment>
- **Title**: HtmlString

---

*Add a screenshot of the component above when available.*
