// Please note: You can set up imports here but they wont be referenced. Please reference your script directly in the html file. Then add the import here. When we port over the work, our import list will be ready.
//import '../styles/style.scss';
import Dropdown from 'bootstrap/js/dist/dropdown';
import Offcanvas from 'bootstrap/js/dist/offcanvas';
import initNavDropdownLabelUpdater from "./components/ToolsAndResources";
import { animateGradient, smoothScrollInternalLinks, expandFirstAccordion } from "./utils";

const featureModules = [
    {
        selector: '[data-hero-carousel]',
        importPath: './components/HeroCarousel',
        init: (mod, elements, selector) => mod.init(elements, selector)
    },
    {
        selector: '.benefit-tabs button[data-bs-toggle="tab"]',
        importPath: './components/BenefitTabs',
        init: (mod, elements) => mod.init(elements)
    },
    {
        selector: '[data-card-stat]',
        importPath: './components/StatCards',
        init: (mod, elements) => mod.init(elements)
    },
]



document.addEventListener('DOMContentLoaded', () => {
    initNavDropdownLabelUpdater('[data-toolsresources]');
    smoothScrollInternalLinks();
    expandFirstAccordion();
    animateGradient("[data-footer]");

    featureModules.forEach(({ selector, importPath, init }) => {
        const elements = document.querySelectorAll(selector);
        if (elements.length > 0) {
            import(importPath)
                .then((mod) => init(mod, elements, selector))
                .catch((err) =>
                    console.error(`Failed to load module for ${selector}:`, err)
                );
        }
    });
});
