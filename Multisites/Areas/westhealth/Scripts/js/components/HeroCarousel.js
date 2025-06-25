import Swiper from 'swiper';
import { Pagination, Navigation, Keyboard } from "swiper/modules";
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import { animateGradient, debounce } from "../utils";

const handleInternalNav = (elem1, elem2) => {
    if (!(elem1 instanceof Element) || !(elem2 instanceof Element)) {
        console.warn('Both arguments must be DOM elements.');
        return;
    }

    if (elem1.nextElementSibling !== elem2) {
        console.warn('internal nav is not a direct (next) sibling. skipping internal nav overlay.');
        return;
    }

    elem2.classList.remove('mt-3');
    elem2.classList.add('position-relative', 'z-3');
    const buttons = elem2.querySelectorAll('a');
    buttons.forEach(button => {
        button.classList.remove('btn-outline-primary');
        button.classList.add('btn-outline-light');
    })

    function getFullHeightWithMargin(el) {
        const styles = getComputedStyle(el);
        const marginTop = parseFloat(styles.marginTop) || 0;
        const marginBottom = parseFloat(styles.marginBottom) || 0;
        return el.offsetHeight + marginTop + marginBottom;
    }

    function applyStacking() {
        const yPaddingOffset = 60; // total padding above/below the nav
        const totalHeight = getFullHeightWithMargin(elem2);
        console.log('new height:', totalHeight);
        elem1.style.paddingBottom = `${totalHeight + yPaddingOffset}px`;
        elem2.style.marginTop = `-${totalHeight + yPaddingOffset}px`;
    }

    applyStacking();

    const debouncedUpdate = debounce(applyStacking, 200);
    window.addEventListener('resize', debouncedUpdate);
}

export const init = (element, moduleSelector) => {
    animateGradient(moduleSelector);
    const swiper = new Swiper('.hero-swiper', {
        modules: [Navigation, Pagination, Keyboard],
        loop: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        slidesPerView: 1,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev'
        }
    });

    const internalNavEl = document.querySelector('.internal-nav');
    const heroEl = document.querySelector(moduleSelector);

    handleInternalNav(heroEl, internalNavEl);
}