import Collapse from 'bootstrap/js/dist/collapse';

export function initNavDropdownLabelUpdater(parentElement) {
    const parent = document.querySelector(parentElement);
    if (!parent) return;
    const label = parent.querySelector("[data-navDropdownLabel]");
    const toggleButton = parent.querySelector('[data-bs-toggle="collapse"][data-bs-target="#navCollapse"]');
    const accordion = parent.querySelector('[data-bs-parent="#navAccordion"]');
    const MOBILE_BREAKPOINT = 767;

    if (!label || !toggleButton || !accordion) return;

    const bsAccordionCollapse = Collapse.getOrCreateInstance(accordion, { toggle: false });

    const allNavLinks = document.querySelectorAll(".nav-link");

    allNavLinks.forEach(btn => {
        btn.addEventListener("click", function () {
            const target = this.getAttribute("data-bs-target");

            // Remove active class from all nav-links
            allNavLinks.forEach(el => el.classList.remove("active"));

            // Set active class for all buttons with same target
            document.querySelectorAll(`.nav-link[data-bs-target="${target}"]`)
                .forEach(el => el.classList.add("active"));

            // Update label
            label.textContent = this.textContent.trim();

            // Collapse mobile accordion (if applicable)
            if (window.innerWidth <= MOBILE_BREAKPOINT) {
                toggleButton.classList.add("collapsed");
                toggleButton.setAttribute("aria-expanded", "false");
                bsAccordionCollapse.hide();
            }
        });
    });

    // Set initial label to active item
    const active = document.querySelector(".nav-link.active");
    if (active) {
        label.textContent = active.textContent.trim();
    }

    // Responsive resize handler
    function updateAccordionState() {
        const isMobile = window.innerWidth < MOBILE_BREAKPOINT;
        const isShown = !toggleButton.classList.contains("collapsed");

        if (isMobile && isShown) {
            bsAccordionCollapse.hide();
            toggleButton.classList.add("collapsed");
            toggleButton.setAttribute("aria-expanded", "false");
        }

        if (!isMobile && !isShown) {
            bsAccordionCollapse.show();
            toggleButton.classList.remove("collapsed");
            toggleButton.setAttribute("aria-expanded", "true");
        }
    }

    window.addEventListener("resize", () => {
        requestAnimationFrame(updateAccordionState);
    });
}

export default initNavDropdownLabelUpdater;