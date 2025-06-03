// Please note: You can set up imports here but they wont be referenced. Please reference your script directly in the html file. Then add the import here. When we port over the work, our import list will be ready.
import '../styles/style.scss';
import initNavDropdownLabelUpdater from "./components/ToolsAndResources";
import {animateGradient, smoothScrollInternalLinks} from "./utils";

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

    animateGradient("[data-footer]");
    animateGradient("[data-hero-carousel]");

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



import gsap from 'gsap';

const bootstrapBreakpoints = {
    xs: 0,
    sm: 576,
    md: 768,
    lg: 992,
    xl: 1200,
    xxl: 1400
};

/**
 * Match a Bootstrap breakpoint using min/lt/gte/gt prefixes.
 * @param {string} queryKey - Example: 'lt-md', 'gte-lg', 'md'
 * @returns {MediaQueryList}
 */
export function matchBreakpoint(queryKey) {
    const match = queryKey.match(/^(lt-|lte-|gt-|gte-)?([a-z]{2,3})$/);

    if (!match) {
        console.warn(`Invalid breakpoint format: "${queryKey}"`);
        return false;
    }

    const [, prefix = '', breakpoint] = match;
    const value = bootstrapBreakpoints[breakpoint];

    if (value == null) {
        console.warn(`Unknown Bootstrap breakpoint: "${breakpoint}"`);
        return false;
    }

    let mediaQuery;

    switch (prefix) {
        case 'lt-': // less than
            mediaQuery = `(max-width: ${value - 0.02}px)`;
            break;
        case 'lte-': // less than or equal
            mediaQuery = `(max-width: ${value}px)`;
            break;
        case 'gt-': // greater than
            mediaQuery = `(min-width: ${value + 0.02}px)`;
            break;
        case 'gte-': // greater than or equal
        case '':     // default: min-width
            mediaQuery = `(min-width: ${value}px)`;
            break;
        default:
            console.warn(`Unknown prefix in breakpoint query: "${prefix}"`);
            return false;
    }

    return window.matchMedia(mediaQuery);
}

export function smoothScrollInternalLinks() {
    document.querySelectorAll('a[href^="#"]').forEach(link => {
        link.addEventListener("click", function (e) {
            e.preventDefault();
            const rawSelector = this.getAttribute("href"); // e.g., "#body" or "#.myclass"
            if (!rawSelector || rawSelector === "#") return;

            const selector = rawSelector.slice(1); // Remove the '#'
            let target =
                document.querySelector(`[data-scrollToTarget='${selector}']`) ||
                document.querySelector(selector.startsWith('.') || selector.startsWith('#') ? selector : `#${selector}`);

            if (target) {
                target.scrollIntoView({ behavior: "smooth" });
            }
        });
    });
}

export function animateGradient(selector) {
    const host = document.querySelector(selector);
    if (!host) return;

    host.classList.add('gradient-host');

    gsap.timeline({ repeat: -1 })
        .to(host, {
            duration: 2.5,
            '--x': '30%', '--y': '70%',
            ease: 'power2.inOut'
        })
        .to(host, {
            duration: 2.5,
            '--x': '100%', '--y': '100%',
            ease: 'circ.inOut'
        })
        .to(host, {
            duration: 4,
            '--x': '0%', '--y': '100%',
            ease: 'sine.inOut'
        })
        .to(host, {
            duration: 2.5,
            '--x': '50%', '--y': '0%',
            ease: 'power3.inOut'
        })
        .to(host, {
            duration: 2.5,
            '--x': '40%', '--y': '50%',
            ease: 'sine.inOut'
        });
}