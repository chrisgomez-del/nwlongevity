import Dropdown from 'bootstrap/js/dist/dropdown';
import Offcanvas from 'bootstrap/js/dist/offcanvas';
import initNavDropdownLabelUpdater from "./components/ToolsAndResources";
import { init } from './components/HeroCarousel';
import {
    animateGradient,
    expandFirstAccordion,
    highlightFooterActiveLink,
    smoothScrollInternalLinks,
    injectFormstackPlaceholders,
    applySecondaryInputAttribute,
    applyFormBtnStyles,
    stripHTMLFromElement
} from "./utils";
import { setupDiagram } from "./components/Diagram";

const featureModules = [
    /*{
        selector: '[data-hero-carousel]',
        importPath: './components/HeroCarousel',
        importFn: () => import('./components/HeroCarousel'),
        init: (mod, elements, selector) => mod.init(elements, selector)
    },*/
    {
        selector: '.benefit-tabs button[data-bs-toggle="tab"]',
        importPath: './components/BenefitTabs',
        importFn: () => import('./components/BenefitTabs'),
        init: (mod, elements) => mod.init(elements)
    },
    {
        selector: '[data-card-stat]',
        importPath: './components/StatCards',
        importFn: () => import('./components/StatCards'),
        init: (mod, elements) => mod.init(elements)
    },
]

document.addEventListener('DOMContentLoaded', () => {
    featureModules.forEach(({ selector, importPath, init, importFn }) => {
        const elements = document.querySelectorAll(selector);
        if (elements.length > 0) {
            importFn()
                .then((mod) => init(mod, elements, selector))
                .catch((err) =>
                    console.error(`Failed to load module for ${selector}:`, err)
                );
        }
    });
    debugger;
    init(null, '[data-hero-carousel]');

    initNavDropdownLabelUpdater('[data-toolsresources]');
    smoothScrollInternalLinks();
    expandFirstAccordion();
    highlightFooterActiveLink('[data-footer] .footer-nav a');
    animateGradient("[data-footer]");

    //setupDiagram();

    const waitForForm = setInterval(() => {
        const form = document.querySelector('#fsform-container-6214449');
        if (form) {
            form.classList.add('formstack-styled');
            injectFormstackPlaceholders();
            applySecondaryInputAttribute();
            applyFormBtnStyles();
            clearInterval(waitForForm);
        }
    }, 100);

    const waitForContactForm = setInterval(() => {
        const form = document.querySelector('#fsform-container-6211076');
        if (form) {
            form.classList.add('formstack-styled');
            applySecondaryInputAttribute('#fsform-container-6211076');
            applyFormBtnStyles('#fsform-container-6211076', 'btn-primary');
            clearInterval(waitForContactForm);
        }
    }, 100);

    document.querySelectorAll('[data-StripHtml]').forEach((element) => {
        element.innerHTML = stripHTMLFromElement(element);
    })
});
