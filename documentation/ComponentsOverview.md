https://www.nm.org/doctors/1396776894/manjot-k-gill-md

# Longevity Components Overview

This document provides a comprehensive overview of all Sitecore MVC components available in the Longevity project. Each component is designed to support the presentation of healthcare and longevity program content in an engaging, interactive format.

## Component Summary

### Interactive Components

#### Flipping Cards Component
- **Purpose**: Displays interactive cards that can be flipped to reveal additional information
- **Use Case**: Features, case studies, or content highlights
- **Template Structure**: FlippingCards → FlippingCard (children)
- **Key Features**: Front/back card content, CTA support, image integration

#### Timeline Component
- **Purpose**: Displays chronological sequence of events with rich media
- **Use Case**: Program milestones, historical events, process steps
- **Template Structure**: Timeline → TimelineEvent (children)
- **Key Features**: Desktop/mobile views, video integration, navigation controls

#### Full Assessment Component
- **Purpose**: Presents comprehensive self-assessment items
- **Use Case**: Multi-organ or multi-topic assessments
- **Template Structure**: FullAssessment → Assessment (children)
- **Key Features**: Separate desktop/mobile videos, structured content presentation

### Media Components

#### Video Carousel Component
- **Purpose**: Showcases collection of videos with mobile/desktop support
- **Use Case**: Introductory or promotional video content
- **Template Structure**: VideoCarousel → Video (children)
- **Key Features**: Mobile device detection, multiple video support, overlay content

#### Right Video Component
- **Purpose**: Combines video background with text content and CTA
- **Use Case**: Visually engaging sections with actionable content
- **Template Structure**: Single RightVideo template
- **Key Features**: Background video, overlay text, call-to-action integration

### Content Components

#### Who We Are Component
- **Purpose**: Flexible sections for organizational storytelling
- **Use Case**: About pages, program descriptions, team introductions
- **Template Structure**: WhoWeAre → WhoWeAreSection (children)
- **Key Features**: Embeddable components (TeamMembers, Timeline), flexible layout

#### Team Members Component
- **Purpose**: Displays structured team member information in rows
- **Use Case**: Staff showcases, contributor listings
- **Template Structure**: TeamMembers → TeamMemberRow → TeamMember (nested children)
- **Key Features**: Hierarchical organization, profile images, linked names

#### Transition Copy Component
- **Purpose**: Presents introductory or transitional content
- **Use Case**: Evidence-based approach sections, process transitions
- **Template Structure**: Single TransitionCopy template
- **Key Features**: Two-section content, CTA integration, mobile-responsive

### Global Templates

#### Settings Template
- **Purpose**: Centralized global configuration for site-wide elements
- **Location**: `/sitecore/content/[site]/home/config/global`
- **Components**: Header and Footer configuration
- **Key Features**: Navigation management, branding, legal information

## Technical Architecture

### Common Patterns
- **Service Layer**: Each component implements a service interface pattern
- **Data Access**: Utilizes SitecoreAccess helper for consistent field retrieval
- **Mobile Support**: Many components include mobile device detection and responsive behavior
- **Media Integration**: Components support both images and videos with proper accessibility
- **Link Management**: Consistent GeneralLink pattern for CTAs and navigation

### Field Types Used
- **HtmlString**: Rich text content with HTML support
- **GeneralLink**: Links with title and URL properties
- **string**: Simple text fields and media paths
- **bool**: Boolean flags for conditional behavior
- **List<T>**: Collections of child items

### Accessibility Features
- ARIA labels and roles
- Keyboard navigation support
- Alt text for images
- Video accessibility attributes

## Usage Guidelines

1. **Component Selection**: Choose components based on content type and interaction requirements
2. **Template Hierarchy**: Follow established parent-child relationships for proper data structure
3. **Mobile Consideration**: Test components across devices, especially those with mobile-specific features
4. **Media Optimization**: Ensure videos and images are optimized for web delivery
5. **Content Strategy**: Use components consistently to maintain user experience coherence

## Development Notes

- All components follow Sitecore MVC patterns
- Services implement dependency injection interfaces
- Mobile device detection uses `HttpContext.Current.Request.Browser.IsMobileDevice`
- Media URLs are generated through SitecoreAccess helper methods
- Field rendering utilizes Sitecore's FieldRenderer for proper output

---

*This overview is current as of the project documentation. Individual component documentation files contain detailed field specifications and implementation details.*