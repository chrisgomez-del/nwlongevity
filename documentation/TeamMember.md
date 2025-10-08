# Team Members Component

The Team Members component is a Sitecore MVC component that displays a structured list of team members, organized into rows. Each team member can have a name (with optional link), title, and profile image. This component is typically used to showcase staff, contributors, or other groups in a visually organized format.

---

![Component Screenshot](PLACEHOLDER_FOR_IMAGE)

---

## Sitecore Template Structure
- A template called **TeamMembers** can have one or more child items called **TeamMemberRow**.
- Each **TeamMemberRow** child item can have one or more child items called **TeamMember**.
- Each **TeamMember** child item represents a single team member and contains the fields listed below.

---

## Fields

### TeamMember
- **Name**: GeneralLink
- **Title**: HtmlString
- **ProfileImagePath**: string

### TeamMemberRow
- **TeamMemberCollection**: List<TeamMember>
- **Class**: string

### TeamMembers
- **TeamMemberRowCollection**: List<TeamMemberRow>

---

*Add a screenshot of the component above when available.*
