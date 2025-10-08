# Who We Are Component

The Who We Are component is a Sitecore MVC component that displays a series of sections describing an organization, team, or program. Each section can include descriptive text, a call-to-action, and dynamic right-side content such as team members, a timeline, or custom HTML. The component is designed to be flexible and support rich layouts for storytelling and organizational introductions.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **WhoWeAre** can have one or more child items called **WhoWeAreSection**.
- Each **WhoWeAreSection** child item represents a single section in the component and contains the fields listed below.
- The right-side content of a section can be populated with Team Members, a Timeline, or custom content, depending on configuration.

---

## Fields

### WhoWeAreSection
- **Class**: string
- **LeftDescription**: HtmlString
- **LeftCTA**: GeneralLink
- **RightContent**: HtmlString
- **RightComponent**: HtmlString
- **RightComponentClass**: string
- **RightComponentDatasource**: string
- **RightTeamMembers**: TeamMembers
- **RightTimeline**: Timeline

### WhoWeAre
- **SectionCollection**: List<WhoWeAreSection>
- **Title**: HtmlString
- **BottomContent**: HtmlString

---

*Add a screenshot of the component above when available.*
